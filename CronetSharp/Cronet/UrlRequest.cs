using System;
using System.Runtime.InteropServices;
using CronetSharp.CronetAsm;

namespace CronetSharp.Cronet
{
    internal static class UrlRequest
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UrlRequest_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequest_Destroy(IntPtr urlRequestPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern EngineResult Cronet_UrlRequest_InitWithParams(
            IntPtr urlRequestPtr, 
            IntPtr enginePtr,
            string url,
            IntPtr urlRequestParamsPtr,
            IntPtr urlRequestCallbackPtr,
            IntPtr executorPtr
        );
        
        [DllImport(CronetLoader.Dll)]
        internal static extern EngineResult Cronet_UrlRequest_Start(IntPtr urlRequestPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern EngineResult Cronet_UrlRequest_FollowRedirect(IntPtr urlRequestPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern EngineResult Cronet_UrlRequest_Read(IntPtr urlRequestPtr, IntPtr bufferPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequest_Cancel(IntPtr urlRequestPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_UrlRequest_IsDone(IntPtr urlRequestPtr);
    
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequest_GetStatus(IntPtr urlRequestPtr, IntPtr urlRequestStatusListenerPtr);
    }
}