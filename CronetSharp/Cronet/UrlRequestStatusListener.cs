using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Asm;

namespace CronetSharp.Cronet
{
    internal static class UrlRequestStatusListener
    {
        internal delegate void OnStatusFunc(IntPtr urlRequestStatusListenerPtr, UrlRequestStatus status);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestStatusListener_Destroy(IntPtr urlRequestStatusListenerPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestStatusListener_OnStatus(IntPtr urlRequestStatusListenerPtr, UrlRequestStatus status);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UrlRequestStatusListener_CreateWith(OnStatusFunc onStatusFunc);
    }
}