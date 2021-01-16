using System;
using System.Runtime.InteropServices;
using CronetSharp.CronetAsm;

namespace CronetSharp.Cronet
{
    internal delegate void ExecuteFunc(IntPtr executorPtr, IntPtr runnablePtr);
    
    internal static class Executor
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Executor_Destroy(IntPtr executorPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Executor_Execute(IntPtr executorPtr, IntPtr runnablePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Executor_CreateWith(ExecuteFunc executeFunc);
    }
}