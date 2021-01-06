using System;
using System.Runtime.InteropServices;

namespace CronetSharp.Wrapper
{
    internal static class Cronet 
    {
        private const string Dll = "cronet.dll";
        
        internal static class Engine
        {
            [DllImport(Dll)]
            internal static extern IntPtr Cronet_Engine_Create();
        }
    }
}