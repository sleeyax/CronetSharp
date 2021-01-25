using System;
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
            var engine = CreateEngine();
            Console.WriteLine($"Engine version: {engine.Version}");

            // run random example
            var examples = new IExample[] { new GetRequestExample(), new PostRequestExample() };
            var example = examples.ElementAt(new Random().Next(0, examples.Length));
            
            example.Run(engine);

            Console.ReadKey();
            example.Dispose();
            engine.Shutdown();
            engine.Dispose();
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