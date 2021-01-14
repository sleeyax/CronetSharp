using System;
using System.Runtime.InteropServices;
using CronetSharp.CronetAsm;

namespace CronetSharp.Cronet
{
    internal class Runnable
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Runnable_Run(IntPtr runnablePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Runnable_Destroy(IntPtr runnablePtr);
    }
}