using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Bin;
using CronetSharp.Cronet.Marshalers;

namespace CronetSharp.Cronet
{
    public class UrlResponseInfo
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UrlResponseInfo_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_Destroy(IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_url_set(
            IntPtr urlResponseInfoPtr,
            string url
        );
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_url_chain_add(
            IntPtr urlResponseInfoPtr,
            string element
        );
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_http_status_code_set(IntPtr urlResponseInfoPtr, int statusCode);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_http_status_text_set(IntPtr urlResponseInfoPtr, string httpStatusText);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_all_headers_list_add(IntPtr urlResponseInfoPtr, IntPtr httpHeaderPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_was_cached_set(IntPtr urlResponseInfoPtr, bool wasCached);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_negotiated_protocol_set(IntPtr urlResponseInfoPtr, string negotiatedProtocol);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_proxy_server_set(IntPtr urlResponseInfoPtr, string proxyServer);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_received_byte_count_set(IntPtr urlResponseInfoPtr, long receivedByteCount);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_UrlResponseInfo_url_get(IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern uint Cronet_UrlResponseInfo_url_chain_size(IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_UrlResponseInfo_url_chain_at(IntPtr urlResponseInfoPtr, uint index);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_url_chain_clear(IntPtr urlResponseInfoPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern int Cronet_UrlResponseInfo_http_status_code_get(IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_UrlResponseInfo_http_status_text_get(IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern uint Cronet_UrlResponseInfo_all_headers_list_size(IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UrlResponseInfo_all_headers_list_at(IntPtr urlResponseInfoPtr, uint index);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UrlResponseInfo_all_headers_list_clear(IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_UrlResponseInfo_was_cached_get(IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_UrlResponseInfo_negotiated_protocol_get(IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_UrlResponseInfo_proxy_server_get(IntPtr urlResponseInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern long Cronet_UrlResponseInfo_received_byte_count_get(IntPtr urlResponseInfoPtr);
    }
}