using System;
using System.Threading;
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

            // create cronet engine
            var engine = CreateEngine();
            Console.WriteLine($"Engine version: {engine.Version}");
            
            // create default executor
            var executor = new Executor();

            // create callback class
            var myUrlRequestCallback = new UrlRequest.Callback.Default();

            // Create and configure a UrlRequest object
            var requestBuilder = engine.NewUrlRequestBuilder("https://sleeyax.dev", myUrlRequestCallback, executor);
            var urlRequest = requestBuilder.Build();

            Console.WriteLine("Starting request..");
            urlRequest.Start();
            
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
            engineParams.Destroy();
            return engine;
        }
    }
}