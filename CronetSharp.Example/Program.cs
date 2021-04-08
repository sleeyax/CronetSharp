using System;
using System.Collections.Generic;
using System.Linq;
using CronetSharp;
using CronetSharp.Cronet.Asm;
using example.Examples;

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
            using var engine = CreateEngine();
            Console.WriteLine($"Engine version: {engine.Version}");

            // run random example
            bool testProxy = false;
            var examples = new List<IExample>() {new GetRequestExample(), new PostRequestExample()};
            if (testProxy) 
                examples.Add(new ProxyRequestExample(new Proxy("127.0.0.1", 8888)));
            var example = examples.ElementAt(new Random().Next(0, examples.Count));
            
            example.Run(engine);

            Console.ReadKey();
            example.Dispose();
            engine.Shutdown();
        }

        static CronetEngine CreateEngine()
        {
            using var engineParams = new CronetEngineParams
            {
                UserAgent = "CronetSample/1",
                BrotliEnabled = true,
            };
            var engine = new CronetEngine(engineParams);
            engine.Start();
            return engine;
        }
    }
}