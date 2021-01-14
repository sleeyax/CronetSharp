using System;
using System.Runtime.InteropServices;
using CronetSharp.CronetAsm;

namespace CronetSharp.Cronet
{
    internal static class Executor
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Executor_Destroy(IntPtr executorPtr);
        
        
    }
}