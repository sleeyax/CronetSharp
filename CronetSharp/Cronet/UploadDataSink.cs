using System;
using System.Runtime.InteropServices;
using CronetSharp.CronetAsm;

namespace CronetSharp.Cronet
{
    internal class UploadDataSink
    {
        internal delegate void OnReadSucceededFunc(IntPtr uploadDataSinkPtr, ulong bytesRead, bool isFinalChunk);
        internal delegate void OnReadErrorFunc(IntPtr uploadDataSinkPtr, string error);
        internal delegate void OnRewindSucceededFunc(IntPtr uploadDataSinkPtr);
        internal delegate void OnRewindErrorFunc(IntPtr uploadDataSinkPtr, string error);

        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UploadDataSink_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UploadDataSink_Destroy(IntPtr uploadDataSinkPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UploadDataSink_OnReadSucceeded(IntPtr uploadDataSinkPtr, ulong bytesRead, bool isFinalChunk);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UploadDataSink_OnReadError(IntPtr uploadDataSinkPtr, string error);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UploadDataSink_OnRewindSucceeded(IntPtr uploadDataSinkPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UploadDataSink_OnRewindError(IntPtr uploadDataSinkPtr, string error);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UploadDataSink_CreateWith(
            OnReadSucceededFunc onReadSucceededFunc,
            OnReadErrorFunc onReadErrorFunc,
            OnRewindSucceededFunc onRewindSucceededFunc,
            OnRewindErrorFunc onRewindErrorFunc
        );
    }
}