using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Bin;
using CronetSharp.Cronet.Marshalers;

namespace CronetSharp.Cronet
{
    internal static class PublicKeyPins
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_PublicKeyPins_Create();
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_PublicKeyPins_Destroy(IntPtr publicKeyPinsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_PublicKeyPins_host_set(IntPtr publicKeyPinsPtr, string host);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_PublicKeyPins_pins_sha256_add(IntPtr publicKeyPinsPtr, string sha256);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_PublicKeyPins_include_subdomains_set(IntPtr publicKeyPinsPtr, bool includeSubdomains);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_PublicKeyPins_expiration_date_set(IntPtr publicKeyPinsPtr, long expirationDate);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_PublicKeyPins_host_get(IntPtr publicKeyPinsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern uint Cronet_PublicKeyPins_pins_sha256_size(IntPtr publicKeyPinsPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_PublicKeyPins_pins_sha256_at(IntPtr publicKeyPinsPtr, uint index);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_PublicKeyPins_pins_sha256_clear(IntPtr publicKeyPinsPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_PublicKeyPins_include_subdomains_get(IntPtr publicKeyPinsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern long Cronet_PublicKeyPins_expiration_date_get(IntPtr publicKeyPinsPtr);
    }
}