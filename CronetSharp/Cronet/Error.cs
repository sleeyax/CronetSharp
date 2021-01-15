using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Marshalers;
using CronetSharp.CronetAsm;

namespace CronetSharp.Cronet
{
    internal static class Error
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Error_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Error_Destroy(IntPtr errorPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Error_error_code_set(IntPtr errorPtr, ErrorCode errorCode);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Error_message_set(IntPtr errorPtr, string message);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Error_internal_error_code_set(IntPtr errorPtr, int internalErrorCode);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Error_immediately_retryable_set(IntPtr errorPtr, bool immediatelyRetryable);

        [DllImport(CronetLoader.Dll)]
        internal static extern ErrorCode Cronet_Error_error_code_get(IntPtr errorPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_Error_message_get(IntPtr errorPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern int Cronet_Error_internal_error_code_get(IntPtr errorPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_Error_immediately_retryable_get(IntPtr errorPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern int Cronet_Error_quic_detailed_error_code_get(IntPtr errorPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Error_quic_detailed_error_code_set(IntPtr errorPtr, int quicDetailedErrorCode);
    }
}