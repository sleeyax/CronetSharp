using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Asm;

namespace CronetSharp.Cronet
{
    internal static class UrlRequestCallback
    {
        internal delegate void OnRedirectReceivedFunc(
            IntPtr urlRequestCallbackPtr,
            IntPtr urlRequestPtr,
            IntPtr urlResponseInfoPtr,
            string newLocationUrl
        );
    
        internal delegate void OnResponseStartedFunc(IntPtr urlRequestCallbackPtr, IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr);
    
        internal delegate void OnReadCompletedFunc(
            IntPtr urlRequestCallbackPtr,
            IntPtr urlRequestPtr,
            IntPtr urlResponseInfoPtr,
            IntPtr bufferPtr,
            ulong bytesRead
        );
    
        internal delegate void OnSucceededFunc(IntPtr urlRequestCallbackPtr, IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr);
    
        internal delegate void OnFailedFunc(
            IntPtr urlRequestCallbackPtr,
            IntPtr urlRequestPtr,
            IntPtr urlResponseInfoPtr,
            IntPtr errorPtr
        );
    
        internal delegate void OnCanceledFunc(IntPtr urlRequestCallbackPtr, IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestCallback_Destroy(IntPtr urlRequestCallbackPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestCallback_OnRedirectReceived(
            IntPtr urlRequestCallbackPtr,
            IntPtr urlRequestPtr,
            IntPtr urlResponseInfoPtr,
            string newLocationUrl
        );
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestCallback_OnResponseStarted(IntPtr urlRequestCallbackPtr, IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestCallback_OnReadCompleted(
            IntPtr urlRequestCallbackPtr,
            IntPtr urlRequestPtr,
            IntPtr urlResponseInfoPtr,
            IntPtr bufferPtr,
            ulong bytesRead
        );
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestCallback_OnSucceeded(IntPtr urlRequestCallbackPtr, IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestCallback_OnFailed(
            IntPtr urlRequestCallbackPtr,
            IntPtr urlRequestPtr,
            IntPtr urlResponseInfoPtr,
            IntPtr errorPtr
        );
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestCallback_OnCanceled(IntPtr urlRequestCallbackPtr, IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UrlRequestCallback_CreateWith(
            OnRedirectReceivedFunc onRedirectReceivedFunc,
            OnResponseStartedFunc onResponseStartedFunc,
            OnReadCompletedFunc onReadCompletedFunc,
            OnSucceededFunc onSucceededFunc,
            OnFailedFunc onFailedFunc,
            OnCanceledFunc onCanceledFunc
        );
    }
}