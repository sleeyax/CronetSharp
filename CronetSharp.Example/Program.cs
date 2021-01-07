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
            var engineParams = GetEngineParams();
            Console.WriteLine($"HTTP2 enabled: {engineParams.Http2Enabled}");
            var engine = new CronetEngine(engineParams);
            Console.WriteLine($"Engine version: {engine.Version}");
        }

        private static CronetEngine BuildEngine()
        {
            var builder = new CronetEngine.Builder()
                .EnableHttp2(false);
            var parameters = builder.GetParams();
            Console.WriteLine($"HTTP2 enabled: {parameters.Http2Enabled}");
            return builder.Build();
        }

        private static CronetEngineParams GetEngineParams()
        {
            return new CronetEngineParams
            {
                Http2Enabled = false,
                HttpCacheMode = HttpCacheMode.InMemory,
                HttpCacheSize = 3000000,
            };
        }
    }
}