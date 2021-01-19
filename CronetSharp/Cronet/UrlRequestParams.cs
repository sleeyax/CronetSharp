using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Marshalers;
using CronetSharp.CronetAsm;

namespace CronetSharp.Cronet
{
    internal static class UrlRequestParams
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UrlRequestParams_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_Destroy(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_http_method_set(
            IntPtr urlRequestParamsPtr, 
            string httpMethod
        );
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_request_headers_add(IntPtr urlRequestParamsPtr, IntPtr httpHeaderPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_disable_cache_set(IntPtr urlRequestParamsPtr, bool disableCache);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_priority_set(IntPtr urlRequestParamsPtr, RequestPriority priority);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_upload_data_provider_set(IntPtr urlRequestParamsPtr, IntPtr uploadDataProviderPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_upload_data_provider_executor_set(IntPtr urlRequestParamsPtr, IntPtr uploadDataProviderExecutorPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_allow_direct_executor_set(IntPtr urlRequestParamsPtr, bool allowDirectExecutor);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_annotations_add(IntPtr urlRequestParamsPtr, IntPtr rawDataPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_request_finished_listener_set(IntPtr urlRequestParamsPtr, IntPtr requestFinishedListener);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_request_finished_executor_set(IntPtr urlRequestParamsPtr, IntPtr requestFinishedExecutor);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_idempotency_set(IntPtr urlRequestParamsPtr, Idempotency idemPotency);

        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_UrlRequestParams_http_method_get(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern uint Cronet_UrlRequestParams_request_headers_size(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern uint Cronet_UrlRequestParams_request_headers_at(IntPtr urlRequestParamsPtr, uint index);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_request_headers_clear(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_UrlRequestParams_disable_cache_get(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern RequestPriority Cronet_UrlRequestParams_priority_get(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UrlRequestParams_upload_data_provider_get(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UrlRequestParams_upload_data_provider_executor_get(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_UrlRequestParams_allow_direct_executor_get(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern uint Cronet_UrlRequestParams_annotations_size(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern uint Cronet_UrlRequestParams_annotations_at(IntPtr urlRequestParamsPtr, uint index);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlRequestParams_annotations_clear(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UrlRequestParams_request_finished_listener_get(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UrlRequestParams_request_finished_executor_get(IntPtr urlRequestParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern Idempotency Cronet_UrlRequestParams_idempotency_get(IntPtr urlRequestParamsPtr);
    }
}