using System;
using CronetSharp;
using CronetSharp.Cronet;
using CronetSharp.CronetAsm;

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {
            // load dll
            ILoader loader = new CronetLoader();
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
                    Console.WriteLine(byteBuffer.GetData());
                    byteBuffer.Destroy(); // TODO: bytebuffer.clear() & reuse same bytebuffer?
                    request.Read(ByteBuffer.Allocate(102400));
                },
                OnSucceeded = (request, info) =>
                {
                    Console.WriteLine("-> succeeded");
                    Console.WriteLine($"Negotiated protocol: {info.NegotiatedProtocol}");
                    Console.WriteLine($"Response status code: {info.HttpStatusCode}");
                    Console.WriteLine($"Amount of response bytes received: {info.ReceivedByteCount}");
                },
                OnFailed = (request, info, error) => Console.WriteLine("-> failed"),
                OnCancelled = (request, info) => Console.WriteLine("-> canceled")
            };

            var myUrlRequestCallback = new UrlRequestCallback(handler);

            // Create and configure a UrlRequest object
            /*var requestBuilder = engine.NewUrlRequestBuilder("https://sleeyax.dev", myUrlRequestCallback, executor);
            var urlRequest = requestBuilder
                .SetHttpMethod("GET")
                .SetPriority(RequestPriority.Highest)
                .Build();
            Console.WriteLine($"request priority : {requestBuilder.GetParams().Priority}");*/
            // alternative syntax:
            var urlRequestParams = new UrlRequestParams
            {
                HttpMethod = "GET",
                Priority = RequestPriority.Highest
            };
            var urlRequest = engine.NewUrlRequest("https://sleeyax.dev", myUrlRequestCallback, executor, urlRequestParams);
            Console.WriteLine($"request priority : {urlRequestParams.Priority}");
            urlRequestParams.Destroy();

            Console.WriteLine("Starting request...");
            urlRequest.Start();

            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
            engine.Shutdown();
            engine.Destroy();
        }

        static CronetEngine CreateEngine()
        {
            var engineParams  = new CronetEngineParams
            {
                UserAgent = "CronetSample/1",
                QuicEnabled = true,
            };
            var engine = new CronetEngine(engineParams);
            engine.Start();
            engineParams.Destroy();
            return engine;
        }
    }
}