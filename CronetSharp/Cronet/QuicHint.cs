using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Bin;
using CronetSharp.Cronet.Marshalers;

namespace CronetSharp.Cronet
{
    internal static class QuicHint
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_QuicHint_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_QuicHint_Destroy(IntPtr quicHintPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_QuicHint_host_set(IntPtr quicHintPtr, string host);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_QuicHint_port_set(IntPtr quicHintPtr, int port);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_QuicHint_alternate_port_set(IntPtr quicHintPtr, int alternatePort);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_QuicHint_host_get(IntPtr quicHintPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern int Cronet_QuicHint_port_get(IntPtr quicHintPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern int Cronet_QuicHint_alternate_port_get(IntPtr quicHintPtr);
    }
}