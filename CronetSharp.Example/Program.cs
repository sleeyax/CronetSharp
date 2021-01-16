using System;
using CronetSharp;
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

            // create callbackk
            var handler = new UrlRequestCallbackHandler
            {
                OnRedirectReceived = (request, info, arg3) =>
                {
                    Console.WriteLine("redirect received");
                    request.FollowRedirect();
                },
                OnResponseStarted = (request, info) =>
                {
                    Console.WriteLine("response started");
                    request.Read(ByteBuffer.Allocate(102400));
                },
                OnReadCompleted = (request, info, byteBuffer, bytesRead) =>
                {
                    Console.WriteLine("read completed");
                    byteBuffer.Destroy(); // TODO: bytebuffer.clear() & reuse same bytebuffer?
                    request.Read(ByteBuffer.Allocate(102400));
                },
                OnSucceeded = (request, info) => Console.WriteLine("succeeded"),
                OnFailed = (request, info, error) => Console.WriteLine("failed"),
                OnCancelled = (request, info) => Console.WriteLine("canceled")
            };

            var myUrlRequestCallback = new UrlRequestCallback(handler);

            // Create and configure a UrlRequest object
            var requestBuilder = engine.NewUrlRequestBuilder("https://sleeyax.dev", myUrlRequestCallback, executor);
            var urlRequest = requestBuilder.SetHttpMethod("GET").Build();

            Console.WriteLine("Starting request..");
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