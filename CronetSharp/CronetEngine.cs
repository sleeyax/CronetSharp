using System;
using CronetSharp.CronetAsm;

namespace CronetSharp
{
    public class CronetEngine
    {
        private readonly IntPtr _enginePtr;
        private readonly IntPtr _engineParamsPtr;
        
        public CronetEngine()
        {
            _enginePtr = Cronet.Engine.Cronet_Engine_Create();
            _engineParamsPtr = Cronet.EngineParams.Cronet_EngineParams_Create();
        }

        public CronetEngine(IntPtr engineParamsPtr)
        {
            _enginePtr = Cronet.Engine.Cronet_Engine_Create();
            _engineParamsPtr = engineParamsPtr;
        }

        public CronetEngine(CronetEngineParams engineParams)
        {
            engineParams.DllLoader.Load();
            _enginePtr = Cronet.Engine.Cronet_Engine_Create();
            _engineParamsPtr = engineParams.Pointer;
        }

        public CronetEngineResult Start()
        {
            return Cronet.Engine.Cronet_Engine_StartWithParams(_enginePtr, _engineParamsPtr);
        }

        /// <summary>
        /// Shuts down the CronetEngine if there are no active requests, otherwise throws an exception.
        /// May block until all the CronetEngine's resources have been cleaned up. 
        /// </summary>
        public void Shutdown()
        {
            Cronet.Engine.Cronet_Engine_Shutdown(_enginePtr);
        }

        /// <summary>
        /// Starts NetLog logging to a file. The NetLog will contain events emitted by all live CronetEngines.
        /// The NetLog is useful for debugging.
        /// The file can be viewed using a Chrome browser navigated to chrome://net-internals/#import
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="logAll"></param>
        public void StartNetLogToFile(string fileName, bool logAll)
        {
            Cronet.Engine.Cronet_Engine_StartNetLogToFile(_enginePtr, fileName, logAll);
        }

        /// <summary>
        /// Stops NetLog logging and flushes file to disk.
        /// If a logging session is not in progress, this call is ignored.
        /// </summary>
        public void StopNetLog()
        {
            Cronet.Engine.Cronet_Engine_StopNetLog(_enginePtr);
        }
        
        public string Version => Cronet.Engine.Cronet_Engine_GetVersionString(_enginePtr);

        public class Builder
        {
            private readonly IntPtr _engineParamsPtr;
            private ILoader _dllLoader;

            /// <summary>
            /// A builder for CronetEngines, which allows runtime configuration of CronetEngine.
            /// Configuration options are set on the builder and then build() is called to create the CronetEngine. 
            /// </summary>
            public Builder()
            {
                _engineParamsPtr = Cronet.EngineParams.Cronet_EngineParams_Create();
                _dllLoader = new CronetLoader();
            }

            public CronetEngine Build()
            {
                _dllLoader.Load();
                return new CronetEngine(_engineParamsPtr);
            }

            // TODO: public key pins
            // TODO: quic(hints)
            
            /// <summary>
            /// Sets whether Brotli compression is enabled.
            /// If enabled, Brotli will be advertised in Accept-Encoding request headers.
            /// Defaults to disabled.
            /// </summary>
            /// <param name="enableBrotli">true to enable Brotli, false to disable.</param>
            /// <returns></returns>
            public Builder EnableBrotli(bool enableBrotli)
            {
                Cronet.EngineParams.Cronet_EngineParams_enable_brotli_set(_engineParamsPtr, enableBrotli);
                return this;
            }

            /// <summary>
            /// Sets whether HTTP/2 protocol is enabled.
            /// Defaults to enabled.
            /// </summary>
            /// <param name="enableHttp2">true to enable HTTP/2, false to disable.</param>
            /// <returns></returns>
            public Builder EnableHttp2(bool enableHttp2)
            {
                Cronet.EngineParams.Cronet_EngineParams_enable_http2_set(_engineParamsPtr, enableHttp2);
                return this;
            }
            
            /// <summary>
            /// Enables or disables caching of HTTP data and other information like QUIC server information.
            /// </summary>
            /// <param name="httpCacheMode">control location and type of cached data.</param>
            /// <param name="maxSize">maximum size in bytes used to cache data (advisory and maybe exceeded at times).</param>
            /// <returns></returns>
            public Builder EnableHttpCache(HttpCacheMode httpCacheMode, long maxSize)
            {
                Cronet.EngineParams.Cronet_EngineParams_http_cache_mode_set(_engineParamsPtr, httpCacheMode);
                Cronet.EngineParams.Cronet_EngineParams_http_cache_max_size_set(_engineParamsPtr, maxSize);
                return this;
            }

            /// <summary>
            /// Enables or disables public key pinning bypass for local trust anchors.
            /// Disabling the bypass for local trust anchors is highly discouraged since it may prohibit the app from communicating with the pinned hosts.
            /// E.g., a user may want to send all traffic through an SSL enabled proxy by changing the device proxy settings and adding the proxy certificate to the list of local trust anchor.
            /// Disabling the bypass will most likely prevent the app from sending any traffic to the pinned hosts.
            /// For more information see 'How does key pinning interact with local proxies and filters?' at //www.chromium.org/Home/chromium-security/security-faq
            /// </summary>
            /// <param name="enablePublicKeyPinningBypassForLocalTrustAnchors">true to enable the bypass, false to disable.</param>
            /// <returns></returns>
            public Builder EnablePublicKeyPinningBypassForLocalTrustAnchors(bool enablePublicKeyPinningBypassForLocalTrustAnchors)
            {
                Cronet.EngineParams.Cronet_EngineParams_enable_public_key_pinning_bypass_for_local_trust_anchors_set(_engineParamsPtr, enablePublicKeyPinningBypassForLocalTrustAnchors);
                return this;
            }

            /// <summary>
            /// Sets whether QUIC protocol is enabled. Defaults to disabled.
            /// If QUIC is enabled, then QUIC User Agent Id containing application name and Cronet version is sent to the server.
            /// </summary>
            /// <param name="enableQuic">true to enable QUIC, false to disable.</param>
            /// <returns></returns>
            public Builder EnableQuic(bool enableQuic)
            {
                Cronet.EngineParams.Cronet_EngineParams_enable_quic_set(_engineParamsPtr, enableQuic);
                return this;
            }

            /// <summary>
            /// Sets a DLL loader to be used to load the native library.
            /// If not set, the default library loader will be used.
            /// </summary>
            /// <param name="loader">Loader to be used to load the native library.</param>
            /// <returns></returns>
            public Builder SetDllLoader(ILoader loader)
            {
                if (loader != null) _dllLoader = loader;
                return this;
            }

            /// <summary>
            /// Sets directory for HTTP Cache and Cookie Storage.
            /// The directory must exist.
            ///
            /// NOTE: Do not use the same storage directory with more than one CronetEngine at a time.
            /// Access to the storage directory does not support concurrent access by multiple CronetEngines. 
            /// </summary>
            /// <param name="storagePath">path to existing directory.</param>
            /// <returns></returns>
            public Builder SetStoragePath(string storagePath)
            {
                Cronet.EngineParams.Cronet_EngineParams_storage_path_set(_engineParamsPtr, storagePath);
                return this;
            }
            
            /// <summary>
            /// Overrides the User-Agent header for all requests.
            /// An explicitly set User-Agent header (set using addHeader(String, String)) will override a value set using this function.
            /// </summary>
            /// <param name="userAgent">the User-Agent string to use for all requests.</param>
            /// <returns></returns>
            public Builder SetUserAgent(string userAgent)
            {
                Cronet.EngineParams.Cronet_EngineParams_user_agent_set(_engineParamsPtr, userAgent);
                return this;
            }
        }
    }
}