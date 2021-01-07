using System;

namespace CronetSharp
{
    public class CronetEngineParams
    {
        public IntPtr Pointer { get; }
        
        public CronetEngineParams()
        {
            Pointer = Cronet.EngineParams.Cronet_EngineParams_Create();
        }
        
        /// <summary>
        /// Destroy the CronetEngineParams and free up memory.
        /// </summary>
        public void Destroy()
        {
            Cronet.EngineParams.Cronet_EngineParams_Destroy(Pointer);
        }

        /// <summary>
        /// Sets whether Brotli compression is enabled.
        /// </summary>
        public bool BrotliEnabled
        {
            get => Cronet.EngineParams.Cronet_EngineParams_enable_brotli_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_enable_brotli_set(Pointer, value);
        }

        /// <summary>
        /// Sets whether HTTP/2 protocol is enabled.
        /// </summary>
        public bool Http2Enabled
        {
            get => Cronet.EngineParams.Cronet_EngineParams_enable_http2_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_enable_http2_set(Pointer, value);
        }
        
        /// <summary>
        /// Enables or disables caching of HTTP data and other information like QUIC server information.
        /// </summary>
        public HttpCacheMode HttpCacheMode
        {
            get => Cronet.EngineParams.Cronet_EngineParams_http_cache_mode_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_http_cache_mode_set(Pointer, value);
        }

        /// <summary>
        /// Maximum size in bytes used to cache data (advisory and maybe exceeded at times).
        /// </summary>
        public long HttpCacheSize
        {
            get => Cronet.EngineParams.Cronet_EngineParams_http_cache_max_size_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_http_cache_max_size_set(Pointer, value);
        }
        
        /// <summary>
        /// Enables or disables public key pinning bypass for local trust anchors.
        /// </summary>
        public bool PublicKeyPinningBypassForLocalTrustAnchorsEnabled
        {
            get => Cronet.EngineParams.Cronet_EngineParams_enable_public_key_pinning_bypass_for_local_trust_anchors_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_enable_public_key_pinning_bypass_for_local_trust_anchors_set(Pointer, value);
        }
        
        /// <summary>
        /// Sets whether QUIC protocol is enabled.
        /// </summary>
        public bool QuicEnabled
        {
            get => Cronet.EngineParams.Cronet_EngineParams_enable_quic_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_enable_quic_set(Pointer, value);
        }
        
        /// <summary>
        /// Sets directory for HTTP Cache and Cookie Storage.
        /// </summary>
        public string StoragePath
        {
            get => Cronet.EngineParams.Cronet_EngineParams_storage_path_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_storage_path_set(Pointer, value);
        }
        
        /// <summary>
        /// Sets the user agent header
        /// </summary>
        public string UserAgent
        {
            get => Cronet.EngineParams.Cronet_EngineParams_user_agent_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_user_agent_set(Pointer, value);
        }
        
        /// <summary>
        /// Sets the accept-language header
        /// </summary>
        public string AcceptLanguage
        {
            get => Cronet.EngineParams.Cronet_EngineParams_accept_language_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_accept_language_set(Pointer, value);
        }
    }
}