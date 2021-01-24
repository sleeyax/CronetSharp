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
        /// <param name="path">absolute path to directory containing cronet.dll</param>
        public virtual void Load(string path = null)
        {
            if (path == null)
            {
                var localPath = new Uri(typeof(Engine).Assembly.EscapedCodeBase).LocalPath;
                var dir = Path.GetDirectoryName(localPath);
                var platform = Environment.Is64BitProcess ? "Win64" : "Win32";
                path = Path.Combine(dir, "Cronet", "Asm", platform);
            }
            
            var asmPath = Path.Combine(path, Dll);

            if (!File.Exists(asmPath)) throw new ArgumentException("Invalid path to DLL specified!");
            
            #if DEBUG
                Console.WriteLine($"Loading {asmPath}");
            #endif
            
            LoadLibrary(asmPath);
        }
    }
}