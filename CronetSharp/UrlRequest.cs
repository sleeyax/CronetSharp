﻿using System;
using System.Drawing;

namespace CronetSharp
{
    public class UrlRequest : IDisposable
    {
        private readonly IntPtr _urlRequestPtr;

        private IntPtr _urlRequestStatusListenerPtr;

        public UrlRequest()
        {
            _urlRequestPtr = Cronet.UrlRequest.Cronet_UrlRequest_Create();
        }
        
        public UrlRequest(IntPtr urlRequestPtr)
        {
            _urlRequestPtr = urlRequestPtr;
        }

        public void Dispose()
        {
            Cronet.UrlRequest.Cronet_UrlRequest_Destroy(_urlRequestPtr);
            GCManager.Free(_urlRequestStatusListenerPtr);
        }

        /// <summary>
        /// Starts the request, all callbacks go to UrlRequest.Callback.
        /// May only be called once.
        /// May not be called if cancel() has been called.
        /// </summary>
        /// <returns></returns>
        public Cronet.EngineResult Start()
        {
            return Cronet.UrlRequest.Cronet_UrlRequest_Start(_urlRequestPtr);
        }

        /// <summary>
        /// Cancels the request.
        /// 
        /// Can be called at any time.
        ///
        /// onCanceled() will be invoked when cancellation is complete and no further callback methods will be invoked.
        /// If the request has completed or has not started, calling cancel() has no effect and onCanceled() will not be invoked.
        /// If the Executor passed in during UrlRequest construction runs tasks on a single thread, and cancel() is called on that thread, no callback methods (besides onCanceled()) will be invoked after cancel() is called.
        /// Otherwise, at most one callback method may be invoked after cancel() has completed. 
        /// </summary>
        /// <returns></returns>
        public void Cancel()
        {
            Cronet.UrlRequest.Cronet_UrlRequest_Cancel(_urlRequestPtr);
        }

        /// <summary>
        /// Follows a pending redirect.
        /// Must only be called at most once for each invocation of onRedirectReceived().
        /// </summary>
        public void FollowRedirect()
        {
            Cronet.UrlRequest.Cronet_UrlRequest_FollowRedirect(_urlRequestPtr);
        }

        /// <summary>
        /// Queries the status of the request.
        /// </summary>
        /// <param name="onStatusChanged"></param>
        public void OnStatusChanged(Action<Cronet.UrlRequestStatus> onStatusChanged)
        {
            Cronet.UrlRequestStatusListener.OnStatusFunc onStatusFunc = (self, status) => onStatusChanged(status);
            var handler = GCManager.Alloc(onStatusFunc);
            _urlRequestStatusListenerPtr = Cronet.UrlRequestStatusListener.Cronet_UrlRequestStatusListener_CreateWith(onStatusFunc);
            GCManager.Register(_urlRequestStatusListenerPtr, handler);
            Cronet.UrlRequest.Cronet_UrlRequest_GetStatus(_urlRequestPtr, _urlRequestStatusListenerPtr);
        }

        /// <summary>
        /// Attempts to read part of the response body into the provided buffer.
        /// Must only be called at most once in response to each invocation of the onResponseStarted() and onReadCompleted() methods of the UrlRequest.Callback.
        /// Each call will result in an asynchronous call to either the Callback's onReadCompleted() method if data is read, its onSucceeded() method if there's no more data to read, or its onFailed() method if there's an error.
        /// </summary>
        /// <param name="buffer"></param>
        public void Read(ByteBuffer buffer)
        {
            Cronet.UrlRequest.Cronet_UrlRequest_Read(_urlRequestPtr, buffer.Pointer);
        }

        /// <summary>
        /// Returns true if the request was successfully started and is now finished (completed, canceled, or failed).
        /// </summary>
        /// <returns></returns>
        public bool IsDone => Cronet.UrlRequest.Cronet_UrlRequest_IsDone(_urlRequestPtr);

        public class Builder
        {
            private readonly IntPtr _urlRequestPtr;

            private readonly UrlRequestParams _urlRequestParams;

            /// <summary>
            /// Function to execute when building is done.
            ///
            /// This finalizes the build and makes sure the UrlRequest uses the parameters we specified.
            /// </summary>
            private readonly Func<Cronet.EngineResult> _onInit;
            
            /// <summary>
            /// Builder for UrlRequests.
            ///
            /// Allows configuring requests before constructing them with build(). 
            /// </summary>
            public Builder()
            {
                _urlRequestParams = new UrlRequestParams();
            }
            
            public Builder(IntPtr urlRequestPtr, IntPtr urlRequestParamsPtr, Func<Cronet.EngineResult> onInit)
            {
                _urlRequestPtr = urlRequestPtr;
                _urlRequestParams = new UrlRequestParams(urlRequestParamsPtr);
                _onInit = onInit;
            }

            /// <summary>
            /// Creates a UrlRequest using configuration within this Builder.
            /// </summary>
            /// <returns></returns>
            public UrlRequest Build()
            {
                _onInit?.Invoke();
                return _urlRequestPtr != default ? new UrlRequest(_urlRequestPtr) : new UrlRequest();
            }

            public UrlRequestParams GetParams()
            {
                return _urlRequestParams;
            }

            /// <summary>
            /// Adds a request header.
            /// </summary>
            /// <param name="header"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            public Builder AddHeader(string header, string value)
            {
                _urlRequestParams.AddHeader(header, value);
                return this;
            }

            /// <summary>
            /// Set the proxy to use for this UrlRequest.
            /// </summary>
            /// <param name="proxy"></param>
            /// <returns></returns>
            public Builder SetProxy(string proxy)
            {
                _urlRequestParams.Proxy = new Proxy(proxy);
                return this;
            }
            
            /// <summary>
            /// Set the proxy to use for this UrlRequest.
            /// </summary>
            /// <param name="proxy"></param>
            /// <returns></returns>
            public Builder SetProxy(Proxy proxy)
            {
                _urlRequestParams.Proxy = proxy;
                return this;
            }
            
            /// <summary>
            /// Sets the request headers.
            /// </summary>
            /// <param name="headers"></param>
            /// <returns></returns>
            public Builder SetHeaders(HttpHeader[] headers)
            {
                _urlRequestParams.Headers = headers;
                return this;
            }
            
            /// <summary>
            /// Marks that the executors this request will use to notify callbacks (for UploadDataProviders and UrlRequest.Callbacks) is intentionally performing inline execution.
            ///
            /// Warning: This option makes it easy to accidentally block the network thread. It should not be used if your callbacks perform disk I/O, acquire locks, or call into other code you don't carefully control and audit. 
            /// </summary>
            /// <param name="allow"></param>
            /// <returns></returns>
            public Builder AllowDirectExecutor(bool allow = true)
            {
                _urlRequestParams.AllowDirectExecutor = allow;
                return this;
            }

            /// <summary>
            /// Disables cache for the request.
            /// </summary>
            public Builder DisableCache(bool disable = true)
            {
                _urlRequestParams.DisableCache = disable;
                return this;
            }

            /// <summary>
            /// Sets the HTTP method verb to use for this request.
            /// 
            /// The default when this method is not called is "GET" if the request has no body or "POST" if it does.
            /// Supported methods: "GET", "HEAD", "DELETE", "POST" or "PUT".
            /// </summary>
            public Builder SetHttpMethod(string method)
            {
                _urlRequestParams.HttpMethod = method;
                return this;
            }

            /// <summary>
            /// Sets priority of the request.
            /// The request is given RequestPriority.Medium priority if this value is not set
            /// </summary>
            public Builder SetPriority(Cronet.RequestPriority priority)
            {
                _urlRequestParams.Priority = priority;
                return this;
            }

            /// <summary>
            /// Sets upload data provider.
            /// </summary>
            /// <param name="provider"></param>
            /// <param name="executor"></param>
            public Builder SetUploadDataProvider(UploadDataProvider provider, Executor executor = null)
            {
                _urlRequestParams.UploadDataProvider = provider;
                if (executor != null) _urlRequestParams.UploadDataProviderExecutor = executor;
                return this;
            }

            /// <summary>
            /// Sets idempotency
            /// </summary>
            /// <param name="idempotency"></param>
            /// <returns></returns>
            public Builder SetIdempotency(Cronet.Idempotency idempotency)
            {
                _urlRequestParams.Idempotency = idempotency;
                return this;
            }

            /// <summary>
            /// Set request finished info listener.
            ///
            /// Warning: the listener will only be called when metrics are set.
            /// </summary>
            /// <param name="listener"></param>
            /// <param name="executor"></param>
            /// <returns></returns>
            public Builder SetRequestFinishedInfoListener(RequestFinishedInfoListener listener, Executor executor)
            {
                _urlRequestParams.RequestFinishedInfoListener = listener;
                _urlRequestParams.RequestFinishedInfoListenerExecutor = executor;
                return this;
            }
        }
    }
}