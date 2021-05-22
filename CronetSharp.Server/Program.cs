using System;
using CronetSharp.Cronet.Bin;
using WebSocketSharp.Server;

namespace CronetSharp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var loader = new CronetLoader();
            var loaded = loader.Load();
            if (!loaded) throw new Exception("Failed to load cronet DLL!");

            string host = "127.0.0.1";
            int port = args.Length > 0 ? Int32.Parse(args[0]) : 8855;
            string uri = $"ws://{host}:{port}";
            
            var wssv = new WebSocketServer (uri);
            
            wssv.AddWebSocketService<CronetService> ("/cronet");
            
            wssv.Start();
            
            Console.WriteLine($"Started listening on {uri}");
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}