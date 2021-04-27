using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Bin;

namespace CronetSharp.Cronet
{
    internal static class RequestFinishedInfoListener
    {
        internal delegate void OnRequestFinishedFunc(
            IntPtr requestFinishedInfoListenerPtr, 
            IntPtr requestFinishedInfoPtr, 
            IntPtr urlResponseInfoPtr,
            IntPtr errorPtr
        );
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_RequestFinishedInfoListener_Destroy(IntPtr requestFinishedInfoListenerPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_RequestFinishedInfoListener_OnRequestFinished(
            IntPtr requestFinishedInfoListenerPtr,
            IntPtr requestFinishedInfoPtr,
            IntPtr urlResponseInfoPtr,
            IntPtr errorPtr
        );
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_RequestFinishedInfoListener_CreateWith(OnRequestFinishedFunc onRequestFinishedFunc);
    }
}