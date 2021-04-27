using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Bin;

namespace CronetSharp.Cronet
{
    internal static class RequestFinishedInfo
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_RequestFinishedInfo_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_RequestFinishedInfo_Destroy(IntPtr requestFinishedInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_RequestFinishedInfo_metrics_set(IntPtr requestFinishedInfoPtr, IntPtr metricsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_RequestFinishedInfo_metrics_move(IntPtr requestFinishedInfoPtr, IntPtr metricsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_RequestFinishedInfo_annotations_add(IntPtr requestFinishedInfoPtr, IntPtr rawDataPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_RequestFinishedInfo_finished_reason_set(IntPtr requestFinishedInfoPtr, RequestFinishedReason reason);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_RequestFinishedInfo_metrics_get(IntPtr requestFinishedInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern uint Cronet_RequestFinishedInfo_annotations_size(IntPtr requestFinishedInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_RequestFinishedInfo_annotations_at(IntPtr requestFinishedInfoPtr, uint index);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_RequestFinishedInfo_annotations_clear(IntPtr requestFinishedInfoPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern RequestFinishedReason Cronet_RequestFinishedInfo_finished_reason_get(IntPtr requestFinishedInfoPtr);
    }
}