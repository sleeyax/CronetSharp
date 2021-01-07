using System;
using CronetSharp;
using CronetSharp.CronetAsm;

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = BuildEngine();
            Console.WriteLine($"Engine version: {engine.Version}");
            Console.ReadKey();
        }

        private static CronetEngine BuildEngine()
        {
            return new CronetEngine.Builder()
                .EnableHttp2(true)
                .EnableHttpCache(HttpCacheMode.InMemory, 3000000)
                .SetDllLoader(new CronetLoader())
                .Build();
        }

        private static CronetEngineParams GetEngineParams()
        {
            return new CronetEngineParams
            {
                Http2Enabled = true,
                HttpCacheMode = HttpCacheMode.InMemory,
                HttpCacheSize = 3000000,
                DllLoader = new CronetLoader(),
            };
        }
    }
}