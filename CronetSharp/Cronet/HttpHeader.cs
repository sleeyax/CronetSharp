using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Asm;
using CronetSharp.Cronet.Marshalers;

namespace CronetSharp.Cronet
{
    internal static class HttpHeader
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_HttpHeader_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_HttpHeader_Destroy(IntPtr httpHeaderPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_HttpHeader_name_set(IntPtr httpHeaderPtr, string name);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_HttpHeader_value_set(IntPtr httpHeaderPtr, string value);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_HttpHeader_name_get(IntPtr httpHeaderPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_HttpHeader_value_get(IntPtr httpHeaderPtr);
        
    }
}