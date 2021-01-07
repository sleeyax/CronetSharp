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

            // create engine
            var engine = CreateEngine();
            Console.WriteLine($"Engine version: {engine.Version}");
            
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