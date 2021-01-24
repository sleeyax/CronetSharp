using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CronetSharp.Cronet.Asm
{
    public class CronetLoader : ILoader
    {
        internal const string Dll = "cronet.dll";
        
        [DllImport("kernel32.dll")]
        protected static extern IntPtr LoadLibrary(string dllToLoad);

        /// <summary>
        /// Loads the cronet dll into the current process
        /// </summary>
        /// <param name="path">relative path to the dll to load</param>
        public virtual void Load(string path)
        {
            var localPath = new Uri(typeof(Engine).Assembly.EscapedCodeBase).LocalPath;
            var dir = Path.GetDirectoryName(localPath);
            var platform = Environment.Is64BitProcess ? "Win64" : "Win32";
            var asmPath = Path.Combine(dir, path, platform, Dll);
            
            #if DEBUG
                Console.WriteLine($"Loading {asmPath}");
            #endif
            
            LoadLibrary(asmPath);
        }
    }
}