using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CronetSharp;
using CronetSharp.Cronet;

namespace example.Examples
{
    public static class MultiThreadingExample
    {
        public static void Run()
        {
            // Start task 1.
            Console.WriteLine("Starting t1");
            var t1 = Task.Run(() =>
            {
                Console.WriteLine("Inside of t1 now");
                return DoRequest("https://httpbin.org/anything", "POST", "{}", new []
                {
                    new HttpHeader("user-agent", "mycustomuseragent"),
                    new HttpHeader("accept", "#1#*"),
                    new HttpHeader("cookie", "abc=def; foo=bar"),
                    new HttpHeader("content-type", "application/json")
                });
            });

            // Start task 2.
            Console.WriteLine("Starting t2");
            var t2 = Task.Run(() =>
            {
                Console.WriteLine("Inside of t2 now");
                return DoRequest("https://httpbin.org/anything", "GET", "", new []
                {
                    new HttpHeader("user-agent", "mycustomuseragent"),
                });
            });
            
            // Wait for task results, one by one.
            Console.WriteLine("waiting for t1");
            Console.WriteLine(t1.Result);
            Console.WriteLine("waiting for t2");
            Console.WriteLine(t2.Result);
        }
        
        public static async Task<string> DoRequest(string url, string method, string body = null, HttpHeader[] headers = null, Proxy proxy = null)
        {
            // This task completion source will contain the response body
            // or an error in case something went wrong.
            // It can also contain null if the request was cancelled by the user.
            var taskCompletionSource = new TaskCompletionSource<string>();

            // Configure and start the cronet engine.
            using var engineParams = new CronetEngineParams
            {
                Http2Enabled = true,
                CheckResultEnabled = false,
            };
            if (proxy != null)  engineParams.Proxy = proxy;
            using var engine = CronetEngine.CreateAndStart(engineParams);
            
            // Create a stream object where response bytes will be copied to.
            using var bodyStream = new MemoryStream();

            using var urlRequestCallback = new UrlRequestCallback
            {
                OnRedirectReceived = (req, info, _) => req.Cancel(),
                OnResponseStarted = (req, info) => req.Read(ByteBuffer.Allocate(102400)),
                OnReadCompleted = (req, info, byteBuffer, bytesRead) =>
                {
                    // Copies response bytes to our response stream, which is created above.
                    var bodyBytes = byteBuffer.GetData();
                    bodyStream.Write(bodyBytes, 0, (int) bytesRead);
                    byteBuffer.Clear();
                    req.Read(byteBuffer);
                },
                OnSucceeded = (request, info) =>
                {
                    try
                    {
                        // Completes the task by setting the response body.
                        string body = Encoding.UTF8.GetString(bodyStream.ToArray());
                        taskCompletionSource.TrySetResult(body);
                    }
                    catch (Exception error)
                    {
                        taskCompletionSource.TrySetException(error);
                    }
                },
                OnCancelled = (request, info) => taskCompletionSource.TrySetCanceled(),
                OnFailed = (req, info, error) => taskCompletionSource.TrySetException(error)
            };

            using var urlRequestParams = new UrlRequestParams
            {
                HttpMethod = method,
                Headers = headers ?? new[] {new HttpHeader("user-agent", "hello kitty")},
                DisableCache = true,
                AllowDirectExecutor = true
            };
            
            // If a request body was specified (e.g. POST request), set the upload provider.
            if (body != null)
            {
                urlRequestParams.UploadDataProvider = UploadDataProvider.Create(body);
                urlRequestParams.UploadDataProviderExecutor = new Executor();
            }

            // Create & start the url request (on a separate single thread).
            using var executor = Executors.NewSingleThreadExecutor();
            using var urlRequest = engine.NewUrlRequest(url, urlRequestCallback, executor, urlRequestParams);
            urlRequest.Start();

            try
            {
                // Wait for the request callback to set the result of the taskCompletionSource
                // so the task can complete.
                var result = await taskCompletionSource.Task;
                
                return result;
            }
            finally
            {
                // try to shutdown the engine
                // if that doesn't work for some reason, throw fatal error
                var engineResult = engine.Shutdown();
                if (engineResult != EngineResult.SUCCESS)
                    throw new Exception($"Failed to shutdown engine. Result: {engineResult}");
            }
        } 
    }
}