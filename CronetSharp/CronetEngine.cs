using System;
using System.Runtime.InteropServices;

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

        public CronetEngine(CronetEngineParams engineParams)
        {
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
        /// Destroy the CronetEngine and free up memory.
        /// </summary>
        public void Destroy()
        {
            Cronet.Engine.Cronet_Engine_Destroy(_enginePtr);
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
        
        public string DefaultUserAgent => Cronet.Engine.Cronet_Engine_GetDefaultUserAgent(_enginePtr);

        public class Builder
        {
            private readonly CronetEngineParams _engineParams;

            /// <summary>
            /// A builder for CronetEngines, which allows runtime configuration of CronetEngine.
            /// Configuration options are set on the builder and then build() is called to create the CronetEngine. 
            /// </summary>
            public Builder()
            {
                _engineParams = new CronetEngineParams();
            }

            public CronetEngine Build()
            {
                return new CronetEngine(_engineParams);
            }

            public CronetEngineParams GetParams()
            {
                return _engineParams;
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
                _engineParams.BrotliEnabled = enableBrotli;
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
                _engineParams.Http2Enabled = enableHttp2;
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
                _engineParams.HttpCacheMode = httpCacheMode;
                _engineParams.HttpCacheSize = maxSize;
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
                _engineParams.PublicKeyPinningBypassForLocalTrustAnchors = enablePublicKeyPinningBypassForLocalTrustAnchors;
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
                _engineParams.Quic = enableQuic;
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
                _engineParams.StoragePath = storagePath;
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
                _engineParams.UserAgent = userAgent;
                return this;
            }

            /// <summary>
            /// Overrides the Accept-Language header for all requests.
            /// An explicitly set Accept-Language header (set using addHeader(String, String)) will override a value set using this function.
            /// </summary>
            /// <param name="userAgent">the accept-language string to use for all requests.</param>
            /// <param name="acceptLanguage"></param>
            /// <returns></returns>
            public Builder SetAcceptLanguage(string acceptLanguage)
            {
                _engineParams.AcceptLanguage = acceptLanguage;
                return this;
            }
        }
    }
}