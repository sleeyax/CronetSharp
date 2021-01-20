using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Asm;

namespace CronetSharp.Cronet
{
    internal static class DateTime
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_DateTime_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_DateTime_Destroy(IntPtr dateTimePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_DateTime_value_set(IntPtr dateTimePtr, long value);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern long Cronet_DateTime_value_get(IntPtr dateTimePtr);
    }
}