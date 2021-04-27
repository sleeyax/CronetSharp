using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Bin;

namespace CronetSharp.Cronet
{
    internal static class Executor
    {
        internal delegate void ExecuteFunc(IntPtr executorPtr, IntPtr runnablePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Executor_Destroy(IntPtr executorPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Executor_Execute(IntPtr executorPtr, IntPtr runnablePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Executor_CreateWith(ExecuteFunc executeFunc);
    }
}