using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CronetSharp.CronetAsm
{
    public static class CronetLoader
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        /// <summary>
        /// Loads the cronet dll into the current process
        /// </summary>
        /// <param name="dll">relative or absolute path to the dll to load</param>
        public static void Load(string dll = "cronet.dll")
        {
            if (Path.IsPathRooted(dll))
            {
                LoadLibrary(dll);
                return;
            }
            
            var path = new Uri(typeof(Wrapper.Cronet).Assembly.EscapedCodeBase).LocalPath;
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