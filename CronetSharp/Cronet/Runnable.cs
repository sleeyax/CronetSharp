using System;
using System.Runtime.InteropServices;
using CronetSharp.CronetAsm;

namespace CronetSharp.Cronet
{
    internal delegate void RunFunc(IntPtr runnablePtr);

    internal class Runnable
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Runnable_Run(IntPtr runnablePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Runnable_Destroy(IntPtr runnablePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Runnable_CreateWith(RunFunc runFunc);
    }
}