﻿using System;
using DateTime = System.DateTime;

namespace CronetSharp
{
    public class CronetEngine : IDisposable
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

        /// <summary>
        /// Factory method that creates and starts a new Cronet Engine
        /// </summary>
        /// <param name="engineParams"></param>
        /// <returns></returns>
        public static CronetEngine CreateAndStart(CronetEngineParams engineParams = null)
        {
            var engine = engineParams != null ? new CronetEngine(engineParams) : new CronetEngine();
            engine.Start();
            return engine;
        }

        /// <summary>
        /// Creates a builder for UrlRequest.
        /// All callbacks for generated UrlRequest objects will be invoked on executor's threads.
        /// executor must not run tasks on the thread calling execute(Runnable) to prevent blocking networking operations and causing exceptions during shutdown.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        /// <param name="executor"></param>
        /// <returns></returns>
        public UrlRequest.Builder NewUrlRequestBuilder(string url, UrlRequestCallback callback, Executor executor)
        {
            IntPtr urlRequestPtr = Cronet.UrlRequest.Cronet_UrlRequest_Create();
            IntPtr urlRequestParamsPtr = Cronet.UrlRequestParams.Cronet_UrlRequestParams_Create();

            Func<Cronet.EngineResult> onInit = () => Cronet.UrlRequest.Cronet_UrlRequest_InitWithParams(
                urlRequestPtr,
                _enginePtr,
                url,
                urlRequestParamsPtr,
                callback.Pointer,
                executor.Pointer
            );

            return new UrlRequest.Builder(urlRequestPtr, urlRequestParamsPtr, onInit);
        }
        
        /// <summary>
        /// Creates a new UrlRequest from given parameters.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        /// <param name="executor"></param>
        /// <param name="urlRequestParams">Readonly url request parameters</param>
        /// <returns></returns>
        public UrlRequest NewUrlRequest(string url, UrlRequestCallback callback, Executor executor, UrlRequestParams urlRequestParams)
        {
            IntPtr urlRequestPtr = Cronet.UrlRequest.Cronet_UrlRequest_Create();

            Cronet.EngineResult result = Cronet.UrlRequest.Cronet_UrlRequest_InitWithParams(
                urlRequestPtr,
                _enginePtr,
                url,
                urlRequestParams.Pointer,
                callback.Pointer,
                executor.Pointer
            );

            return new UrlRequest(urlRequestPtr);
        }

        public Cronet.EngineResult Start()
        {
            return Cronet.Engine.Cronet_Engine_StartWithParams(_enginePtr, _engineParamsPtr);
        }

        /// <summary>
        /// Shuts down the CronetEngine if there are no active requests, otherwise throws an exception.
        /// May block until all the CronetEngine's resources have been cleaned up. 
        /// </summary>
        public Cronet.EngineResult Shutdown()
        {
            return Cronet.Engine.Cronet_Engine_Shutdown(_enginePtr);
        }
        
        /// <summary>
        /// Destroy the CronetEngine and free up memory.
        /// </summary>
        public void Dispose()
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
        public bool StartNetLogToFile(string fileName, bool logAll)
        {
            return Cronet.Engine.Cronet_Engine_StartNetLogToFile(_enginePtr, fileName, logAll);
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
            
            public CronetEngine BuildAndStart()
            {
                var engine = new CronetEngine(_engineParams);
                engine.Start();
                return engine;
            }

            public CronetEngineParams GetParams()
            {
                return _engineParams;
            }

            /// <summary>
            /// Pins a set of public keys for a given host.
            /// </summary>
            /// <returns></returns>
            public Builder AddPublicKeyPins(string hostname, string[] pinsSha256, bool includeSubdomains, DateTime expirationDate)
            {
                _engineParams.AddPublicKeyPins(new PublicKeyPins(hostname, pinsSha256, includeSubdomains, expirationDate));
                return this;
            }

            /// <summary>
            /// Adds hint that a host supports QUIC.
            /// </summary>
            /// <param name="host"></param>
            /// <param name="port"></param>
            /// <param name="alternatePort"></param>
            /// <returns></returns>
            public Builder AddQuicHint(string host, int port, int alternatePort)
            {
                _engineParams.AddQuicHint(new QuicHint(host, port, alternatePort));
                return this;
            }
            
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
            public Builder EnableHttpCache(Cronet.HttpCacheMode httpCacheMode, long maxSize)
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
                _engineParams.PublicKeyPinningBypassForLocalTrustAnchorsEnabled = enablePublicKeyPinningBypassForLocalTrustAnchors;
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
                _engineParams.QuicEnabled = enableQuic;
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
            /// Set Proxy to use for each request
            /// </summary>
            /// <param name="proxy"></param>
            /// <returns></returns>
            public Builder SetProxy(string proxy)
            {
                _engineParams.Proxy = new Proxy(proxy);
                return this;
            }
            
            /// <summary>
            /// Set Proxy to use for each request
            /// </summary>
            /// <param name="proxy"></param>
            /// <returns></returns>
            public Builder SetProxy(Proxy proxy)
            {
                _engineParams.Proxy = proxy;
                return this;
            }

            /// <summary>
            /// Overrides the Accept-Language header for all requests.
            /// An explicitly set Accept-Language header (set using addHeader(String, String)) will override a value set using this function.
            /// </summary>
            /// <param name="acceptLanguage">the accept-language string to use for all requests.</param>
            /// <returns></returns>
            public Builder SetAcceptLanguage(string acceptLanguage)
            {
                _engineParams.AcceptLanguage = acceptLanguage;
                return this;
            }

            /// <summary>
            /// Disable runtime CHECK of the result
            /// </summary>
            /// <param name="disable"></param>
            /// <returns></returns>
            public Builder DisableCheckResult(bool disable = true)
            {
                _engineParams.CheckResultEnabled = !disable;
                return this;
            }

            /// <summary>
            /// Sets experimental cronet options.
            /// You can provide Chromium flags in JSON format.
            /// For more information, see Chromium/Chrome documentation.
            /// </summary>
            /// <param name="options"></param>
            /// <returns></returns>
            public Builder SetExperimentalOptions(string options)
            {
                _engineParams.ExperimentalOptions = options;
                return this;
            }

            /// <summary>
            /// Sets thread priority.
            /// </summary>
            /// <param name="threadPriority"></param>
            /// <returns></returns>
            public Builder SetThreadPriority(double threadPriority)
            {
                _engineParams.ThreadPriority = threadPriority;
                return this;
            }
        }
    }
}