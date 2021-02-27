using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Asm;
using CronetSharp.Cronet.Marshalers;

namespace CronetSharp.Cronet
{
    internal static class EngineParams
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_EngineParams_Create();

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_Destroy(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_EngineParams_accept_language_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_accept_language_set(IntPtr engineParamsPtr, string acceptLanguage);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_EngineParams_enable_brotli_get(IntPtr engineParamsPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_enable_brotli_set(IntPtr engineParamsPtr, bool enableBrotli);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_EngineParams_enable_check_result_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_enable_check_result_set(IntPtr engineParamsPtr, bool enableCheckResult);

        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_EngineParams_enable_http2_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_enable_http2_set(IntPtr engineParamsPtr, bool enableHttp2);

        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_EngineParams_enable_quic_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_enable_quic_set(IntPtr engineParamsPtr, bool enableQuic);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_EngineParams_experimental_options_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_experimental_options_set(IntPtr engineParamsPtr, string experimentalOptions);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern long Cronet_EngineParams_http_cache_max_size_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_http_cache_max_size_set(IntPtr engineParamsPtr, long maxCacheSize);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern HttpCacheMode Cronet_EngineParams_http_cache_mode_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_http_cache_mode_set(IntPtr engineParamsPtr, HttpCacheMode httpCacheMode);

        [DllImport(CronetLoader.Dll)]
        internal static extern double Cronet_EngineParams_network_thread_priority_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_network_thread_priority_set(IntPtr engineParamsPtr, double threadPriority);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Cronet_EngineParams_enable_public_key_pinning_bypass_for_local_trust_anchors_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_enable_public_key_pinning_bypass_for_local_trust_anchors_set(IntPtr engineParamsPtr, bool enablePublicKeyPinningBypassForLocalTrustAnchors);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_public_key_pins_add(IntPtr engineParamsPtr, IntPtr publicKeyPinsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_EngineParams_public_key_pins_at(IntPtr engineParamsPtr, uint index);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_public_key_pins_clear(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern uint Cronet_EngineParams_public_key_pins_size(IntPtr engineParamsPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_quic_hints_add(IntPtr engineParamsPtr, IntPtr quickHints);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_EngineParams_quic_hints_at(IntPtr engineParamsPtr, uint index);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_quic_hints_clear(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern uint Cronet_EngineParams_quic_hints_size(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_EngineParams_storage_path_get(IntPtr engineParamsPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_storage_path_set(IntPtr engineParamsPtr, string storagePath);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_EngineParams_user_agent_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_user_agent_set(IntPtr engineParamsPtr, string userAgent);
        
        [DllImport(CronetLoader.Dll)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        internal static extern string Cronet_EngineParams_proxy_get(IntPtr engineParamsPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_EngineParams_proxy_set(IntPtr engineParamsPtr, string proxy);
    }
}