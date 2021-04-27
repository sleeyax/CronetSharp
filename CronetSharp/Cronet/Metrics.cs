using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Bin;

namespace CronetSharp.Cronet
{
    internal static class Metrics
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_Destroy(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_request_start_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_request_start_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_dns_start_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_dns_start_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_dns_end_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_dns_end_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_connect_start_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_connect_start_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_connect_end_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_connect_end_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_ssl_start_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_ssl_start_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_ssl_end_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_ssl_end_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_sending_start_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_sending_start_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_sending_end_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_sending_end_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_push_start_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_push_start_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_push_end_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_push_end_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_response_start_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_response_start_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_request_end_set(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_request_end_move(IntPtr metricPtr, IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_socket_reused_set(IntPtr metricPtr, bool reused);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_sent_byte_count_set(IntPtr metricPtr, long byteCount);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Metrics_received_byte_count_set(IntPtr metricPtr, long byteCount);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_request_start_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_dns_start_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_dns_end_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_connect_start_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_connect_end_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_ssl_start_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_ssl_end_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_sending_start_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_sending_end_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_push_start_get(IntPtr metricPtr);
        
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_push_end_get(IntPtr metricPtr);
        
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_response_start_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Metrics_request_end_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_Metrics_socket_reused_get(IntPtr metricPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern long Cronet_Metrics_sent_byte_count_get(IntPtr metricPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern long Cronet_Metrics_received_byte_count_get(IntPtr metricPtr);
    }
}