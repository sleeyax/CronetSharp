using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CronetSharp.Cronet.Bin
{
    public class CronetLoader : ILoader
    {
        internal const string Dll = "cronet.dll";
        
        [DllImport("kernel32.dll", SetLastError = true, ThrowOnUnmappableChar = true)]
        protected static extern IntPtr LoadLibrary(string dllToLoad);

        /// <summary>
        /// Loads the cronet dll into the current process
        /// </summary>
        /// <param name="path">absolute path to directory containing cronet.dll</param>
        public virtual bool Load(string path = null)
        {
            if (path == null)
            {
                var localPath = new Uri(typeof(Engine).Assembly.EscapedCodeBase).LocalPath;
                var dir = Path.GetDirectoryName(localPath);
                var platform = Environment.Is64BitProcess ? "Win64" : "Win32";
                path = Path.Combine(dir, "Cronet", "Bin", platform);
            }
            
            var binPath = Path.Combine(path, Dll);

            if (!File.Exists(binPath)) throw new ArgumentException("Invalid path to DLL specified!");
            
            #if DEBUG
                Console.WriteLine($"Loading {binPath}");
            #endif
            
            LoadLibrary(binPath);
            
            int errorCode = Marshal.GetLastWin32Error();

            if (errorCode != 0)
            {
                #if DEBUG
                    Console.WriteLine($"Failed to load DLL. Error code: {errorCode}");
                #endif
                return false;
            }

            return true;
        }
    }
}