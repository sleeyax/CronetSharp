using System;
using System.IO;
using System.Runtime.InteropServices;
using CronetSharp.Cronet;

namespace CronetSharp.CronetAsm
{
    public class CronetLoader : ILoader
    {
        internal const string Dll = "cronet.dll";
        
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        /// <summary>
        /// Loads the cronet dll into the current process
        /// </summary>
        /// <param name="dll">relative path to the dll to load</param>
        public void Load(string dll = Dll)
        {
            var path = new Uri(typeof(Engine).Assembly.EscapedCodeBase).LocalPath;
            var dir = Path.GetDirectoryName(path);
            var platform = Environment.Is64BitProcess ? "Win64" : "Win32";
            var asmPath = Path.Combine(dir, "CronetAsm", platform, dll);
            
            #if DEBUG
                Console.WriteLine($"Loading {asmPath}");
            #endif
            
            LoadLibrary(asmPath);
        }
    }
}