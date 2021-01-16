using System;
using System.Collections.Generic;
using System.Linq;

namespace CronetSharp
{
    public class UrlRequest
    {
        private readonly IntPtr _urlRequestPtr;

        public UrlRequest()
        {
            _urlRequestPtr = Cronet.UrlRequest.Cronet_UrlRequest_Create();
        }
        
        public UrlRequest(IntPtr urlRequestPtr)
        {
            _urlRequestPtr = urlRequestPtr;
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
        /// Attempts to read part of the response body into the provided buffer.
        /// Must only be called at most once in response to each invocation of the onResponseStarted() and onReadCompleted() methods of the UrlRequest.Callback.
        /// Each call will result in an asynchronous call to either the Callback's onReadCompleted() method if data is read, its onSucceeded() method if there's no more data to read, or its onFailed() method if there's an error.
        /// </summary>
        /// <param name="buffer"></param>
        public void Read(ByteBuffer buffer)
        {
            Cronet.UrlRequest.Cronet_UrlRequest_Read(_urlRequestPtr, buffer.Pointer);
        }
        // TODO: getStatus(Listener)
        
        /// <summary>
        /// Returns true if the request was successfully started and is now finished (completed, canceled, or failed).
        /// </summary>
        /// <returns></returns>
        public bool IsDone()
        {
            return Cronet.UrlRequest.Cronet_UrlRequest_IsDone(_urlRequestPtr);
        }

        public class Builder
        {
            private readonly IntPtr _urlRequestPtr;

            private readonly UrlRequestParams _urlRequestParams;
            
            /// <summary>
            /// Builder for UrlRequests.
            ///
            /// Allows configuring requests before constructing them with build(). 
            /// </summary>
            public Builder()
            {
                _urlRequestParams = new UrlRequestParams();
            }
            
            public Builder(IntPtr urlRequestPtr, IntPtr urlRequestParamsPtr)
            {
                _urlRequestPtr = urlRequestPtr;
                _urlRequestParams = new UrlRequestParams(urlRequestParamsPtr);
            }

            /// <summary>
            /// Creates a UrlRequest using configuration within this Builder.
            /// </summary>
            /// <returns></returns>
            public UrlRequest Build()
            {
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
            /// Sets the request headers.
            /// </summary>
            /// <param name="headers"></param>
            /// <returns></returns>
            public Builder SetHeaders(Dictionary<string, string> headers)
            {
                _urlRequestParams.SetHeaders(headers);
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
                string[] supported = {"GET", "HEAD", "DELETE", "POST", "PUT"};
                
                method = method.ToUpper();
                
                if (!supported.Contains(method))
                    throw new ArgumentException($"Method {method} is not supported! Must be one of {string.Join(", ", supported)}"); 
                
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
        }
    }
}