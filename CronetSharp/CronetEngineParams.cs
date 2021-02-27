using System;
using System.Collections.Generic;

namespace CronetSharp
{
    public class CronetEngineParams : IDisposable
    {
        public IntPtr Pointer { get; }
        
        public CronetEngineParams()
        {
            Pointer = Cronet.EngineParams.Cronet_EngineParams_Create();
        }
        
        /// <summary>
        /// Destroy the CronetEngineParams and free up memory.
        /// </summary>
        public void Dispose()
        {
            Cronet.EngineParams.Cronet_EngineParams_Destroy(Pointer);
        }

        /// <summary>
        /// Set public key pins
        /// </summary>
        public PublicKeyPins[] PublicKeyPins
        {
            get
            {
                var size = Cronet.EngineParams.Cronet_EngineParams_public_key_pins_size(Pointer);
                var publicKeyPins = new PublicKeyPins[size];

                for (uint i = 0; i < size; i++)
                {
                    var publicKeyPinPointer = Cronet.EngineParams.Cronet_EngineParams_public_key_pins_at(Pointer, i);
                    publicKeyPins[i] = new PublicKeyPins(publicKeyPinPointer);
                }

                return publicKeyPins;
            }
            set
            {
                foreach (var publicKeyPins in value)
                    AddPublicKeyPins(publicKeyPins);
            }
        }
        
        /// <summary>
        /// Set QUIC hints
        /// </summary>
        public QuicHint[] QuicHints
        {
            get
            {
                var size = Cronet.EngineParams.Cronet_EngineParams_quic_hints_size(Pointer);
                var quicHints = new QuicHint[size];

                for (uint i = 0; i < size; i++)
                {
                    var quicHintPointer = Cronet.EngineParams.Cronet_EngineParams_quic_hints_at(Pointer, i);
                    quicHints[i] = new QuicHint(quicHintPointer);
                }

                return quicHints;
            }
            set
            {
                foreach (var quicHint in value)
                    AddQuicHint(quicHint);
            }
        }

        /// <summary>
        /// Pins a set of public keys for a given host.
        /// </summary>
        public void AddPublicKeyPins(PublicKeyPins publicKeyPins)
        {
            Cronet.EngineParams.Cronet_EngineParams_public_key_pins_add(Pointer, publicKeyPins.Pointer);
        }

        public void ClearPublicKeyPins()
        {
            Cronet.EngineParams.Cronet_EngineParams_public_key_pins_clear(Pointer);
        }

        /// <summary>
        /// Adds hint that a host supports QUIC.
        /// </summary>
        /// <param name="quicHint"></param>
        public void AddQuicHint(QuicHint quicHint)
        {
            Cronet.EngineParams.Cronet_EngineParams_quic_hints_add(Pointer, quicHint.Pointer);
        }

        public void ClearQuicHints()
        {
            Cronet.EngineParams.Cronet_EngineParams_quic_hints_clear(Pointer);
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
        public Cronet.HttpCacheMode HttpCacheMode
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
        /// Sets the proxy to use
        /// </summary>
        public Proxy Proxy
        {
            get => new Proxy(Cronet.EngineParams.Cronet_EngineParams_proxy_get(Pointer));
            set => Cronet.EngineParams.Cronet_EngineParams_proxy_set(Pointer, value.Format(ProxyFormat.ReverseNotation));
        }
        
        /// <summary>
        /// Sets the accept-language header
        /// </summary>
        public string AcceptLanguage
        {
            get => Cronet.EngineParams.Cronet_EngineParams_accept_language_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_accept_language_set(Pointer, value);
        }

        /// <summary>
        /// Enable runtime CHECK of the result.
        /// </summary>
        public bool CheckResultEnabled
        {
            get => Cronet.EngineParams.Cronet_EngineParams_enable_check_result_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_enable_check_result_set(Pointer, value);
        }

        /// <summary>
        /// Sets experimental cronet options.
        /// </summary>
        public string ExperimentalOptions
        {
            get => Cronet.EngineParams.Cronet_EngineParams_experimental_options_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_experimental_options_set(Pointer, value);
        }

        /// <summary>
        /// Sets thread priority.
        /// </summary>
        public double ThreadPriority
        {
            get => Cronet.EngineParams.Cronet_EngineParams_network_thread_priority_get(Pointer);
            set => Cronet.EngineParams.Cronet_EngineParams_network_thread_priority_set(Pointer, value);
        }
    }
}