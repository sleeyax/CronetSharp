using System;
using System.Collections.Generic;
using System.IO;
using CronetSharp;
using CronetSharp.Cronet;
using CronetSharp.Cronet.Asm;

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {
            // load dll
            var loader = new CronetLoader();
            loader.Load();

            // create & start cronet engine
            var engine = CreateEngine();
            Console.WriteLine($"Engine version: {engine.Version}");
            
            // create default executor
            var executor = new Executor();

            // create callback
            var handler = new UrlRequestCallbackHandler
            {
                OnRedirectReceived = (request, info, arg3) =>
                {
                    Console.WriteLine("-> redirect received");
                    request.FollowRedirect();
                },
                OnResponseStarted = (request, info) =>
                {
                    Console.WriteLine("-> response started");
                    request.Read(ByteBuffer.Allocate(102400));
                },
                OnReadCompleted = (request, info, byteBuffer, bytesRead) =>
                {
                    Console.WriteLine("-> read completed");
                    Console.WriteLine(byteBuffer.GetDataAsString());
                    byteBuffer.Clear();
                    request.Read(byteBuffer);
                },
                OnSucceeded = (request, info) =>
                {
                    Console.WriteLine("-> succeeded");
                    Console.WriteLine($"Negotiated protocol: {info.NegotiatedProtocol}");
                    Console.WriteLine($"Response Status code: {info.HttpStatusCode}");
                    Console.WriteLine($"Response Headers: ");
                    foreach (var header in info.Headers)
                        Console.WriteLine($"{header.Name}:{header.Value}");
                    Console.WriteLine($"Response bytes received: {info.ReceivedByteCount}");
                },
                OnFailed = (request, info, error) => Console.WriteLine("-> failed"),
                OnCancelled = (request, info) => Console.WriteLine("-> canceled")
            };

            var myUrlRequestCallback = new UrlRequestCallback(handler);

            // Create and configure a UrlRequest object
            // send GET request
            var getRequestParams = new UrlRequestParams
            {
                HttpMethod = "GET",
                Priority = RequestPriority.Highest,
                Headers = new List<HttpHeader>
                {
                    new HttpHeader("user-agent", "mycustomuseragent"),
                    new HttpHeader("accept", "*/*"),
                    new HttpHeader("cookie", "abc=def; foo=bar"),
                    // new HttpHeader("cookie", "foo=bar")
                }
            };
            var getRequest = engine.NewUrlRequest("https://httpbin.org/anything", myUrlRequestCallback, executor, getRequestParams);
            getRequestParams.Destroy();

            Console.WriteLine("Starting GET request...");
            getRequest.Start();
            // query status of the request (note: this is not a 'callback setter' so it will only log a value once)
            // getRequest.GetStatus(status => Console.WriteLine($"Current request status: {status}"));

            // send POST request (using alternative builder syntax)
            var postRequestBuilder = engine.NewUrlRequestBuilder("https://httpbin.org/anything", myUrlRequestCallback, executor)
                .SetHttpMethod("POST")
                .AddHeader("content-type", "application/json")
                .AddHeader("my-custom-header", "customvalue")
                .SetUploadDataProvider(UploadDataProvider.Create("{}"), executor);
            var postRequest = postRequestBuilder.Build();
            postRequestBuilder.GetParams().Destroy(); // free up now unnecessary resources from unmanaged memory
            Console.WriteLine("Starting POST request...");
            postRequest.Start();

            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
            getRequest.Destroy();
            postRequest.Destroy();
            engine.Shutdown();
            engine.Destroy();
        }

        static CronetEngine CreateEngine()
        {
            var engineParams  = new CronetEngineParams
            {
                UserAgent = "CronetSample/1",
                BrotliEnabled = true,
            };
            var engine = new CronetEngine(engineParams);
            engine.Start();
            engineParams.Destroy();
            return engine;
        }
    }
}