using System;
using System.Drawing;
using CronetSharp.CronetAsm;

namespace CronetSharp
{
    public class CronetEngineParams
    {
        public IntPtr Pointer { get; }

        private ILoader _dllLoader;

        public CronetEngineParams()
        {
            Pointer = Cronet.EngineParams.Cronet_EngineParams_Create();
            _dllLoader = new CronetLoader();
        }
        
        public CronetEngineParams(ILoader dllLoader)
        {
            Pointer = Cronet.EngineParams.Cronet_EngineParams_Create();
            _dllLoader = dllLoader;
        }

        /// <summary>
        /// Sets a CronetEngine.Builder.LibraryLoader to be used to load the native library.
        /// </summary>
        public ILoader DllLoader
        {
            get => _dllLoader;
            set {
                if (value != null)
                    _dllLoader = value;
            }
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
        public bool PublicKeyPinningBypassForLocalTrustAnchors
        {
            get => Cronet.EngineParams.Cronet_EngineParams_enable_public_key_pinning_bypass_for_local_trust_anchors_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_enable_public_key_pinning_bypass_for_local_trust_anchors_set(Pointer, value);
        }
        
        /// <summary>
        /// Sets whether QUIC protocol is enabled.
        /// </summary>
        public bool Quic
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
        /// Constructs a User-Agent string including application name and version, system build version, model and id, and Cronet version.
        /// </summary>
        public string UserAgent
        {
            get => Cronet.EngineParams.Cronet_EngineParams_user_agent_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_user_agent_set(Pointer, value);
        }
    }
}