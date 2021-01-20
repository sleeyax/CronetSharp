using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Asm;

namespace CronetSharp.Cronet
{
    internal static class BufferCallback
    {
        internal delegate void OnDestroyFunc(IntPtr bufferCallbackPtr, IntPtr bufferPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_BufferCallback_Destroy(IntPtr bufferCallbackPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_BufferCallback_OnDestroy(IntPtr bufferCallbackPtr, IntPtr bufferPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_BufferCallback_CreateWith(OnDestroyFunc onDestroyFunc);
    }
}