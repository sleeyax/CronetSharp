using System;
using CronetSharp;
using CronetSharp.CronetAsm;

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {
            CronetLoader.Load();
            
            var engine = new CronetEngine();

            Console.ReadKey();
        }
    }
}