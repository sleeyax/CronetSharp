using System;
using System.Collections.Generic;
using System.Linq;

namespace CronetSharp
{
    public class UrlRequest
    {
        private readonly IntPtr _urlRequestPtr;
        private readonly IntPtr _urlRequestParamsPtr;

        public UrlRequest()
        {
            _urlRequestPtr = Cronet.UrlRequest.Cronet_UrlRequest_Create();
            _urlRequestParamsPtr = Cronet.UrlRequestParams.Cronet_UrlRequestParams_Create();
        }
        
        public UrlRequest(IntPtr urlRequestPtr)
        {
            _urlRequestPtr = urlRequestPtr;
            _urlRequestParamsPtr = Cronet.UrlRequestParams.Cronet_UrlRequestParams_Create();
        }
        
        public UrlRequest(UrlRequestParams urlRequestParams)
        {
            _urlRequestPtr = Cronet.UrlRequest.Cronet_UrlRequest_Create();
            _urlRequestParamsPtr = urlRequestParams.Pointer;
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

            /// <summary>
            /// Creates a UrlRequest using configuration within this Builder.
            /// </summary>
            /// <returns></returns>
            public UrlRequest Build()
            {
                return new UrlRequest(_urlRequestParams);
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

        /// <summary>
        /// Users of Cronet extend this class to receive callbacks indicating the progress of a UrlRequest being processed.
        /// An instance of this class is passed in to UrlRequest.Builder's constructor when constructing the UrlRequest.
        /// 
        /// Note: All methods will be invoked on the thread of the Executor used during construction of the UrlRequest.
        /// </summary>
        public abstract class Callback : Cronet.UrlRequestCallback
        {
            /// <summary>
            /// Invoked if request was canceled via cancel().
            /// Once invoked, no other UrlRequest.Callback methods will be invoked.
            /// Default implementation takes no action.
            /// </summary>
            public virtual void OnCanceled(UrlRequest request, UrlResponseInfo info) { }

            /// <summary>
            /// Invoked if request failed for any reason after start().
            /// Once invoked, no other UrlRequest.Callback methods will be invoked.
            /// error provides information about the failure.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="info"></param>
            /// <param name="error"></param>
            public abstract void OnFailed(UrlRequest request, UrlResponseInfo info, CronetException error);

            /// <summary>
            /// Invoked whenever part of the response body has been read.
            /// Only part of the buffer may be populated, even if the entire response body has not yet been consumed.
            /// With the exception of onCanceled(), no other UrlRequest.Callback method will be invoked for the request, including onSucceeded() and onFailed(), until UrlRequest.read() is called to attempt to continue reading the response body.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="info"></param>
            /// <param name="byteBuffer"></param>
            public abstract void OnReadCompleted(UrlRequest request, UrlResponseInfo info, ByteBuffer byteBuffer);

            /// <summary>
            /// Invoked whenever a redirect is encountered. This will only be invoked between the call to start() and onResponseStarted().
            /// The body of the redirect response, if it has one, will be ignored.
            /// The redirect will not be followed until the URLRequest's followRedirect() method is called, either synchronously or asynchronously.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="info"></param>
            /// <param name="newLocationUrl"></param>
            public abstract void OnRedirectReceived(UrlRequest request, UrlResponseInfo info, String newLocationUrl);

            /// <summary>
            /// Invoked when the final set of headers, after all redirects, is received. Will only be invoked once for each request.
            /// With the exception of onCanceled(), no other UrlRequest.Callback method will be invoked for the request, including onSucceeded() and onFailed(), until UrlRequest.read() is called to attempt to start reading the response body.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="info"></param>
            public abstract void OnResponseStarted(UrlRequest request, UrlResponseInfo info);

            /// <summary>
            /// Invoked when request is completed successfully.
            /// Once invoked, no other UrlRequest.Callback methods will be invoked.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="info"></param>
            public abstract void OnSucceeded(UrlRequest request, UrlResponseInfo info);

            internal override void OnCanceled(IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr)
            {
                OnCanceled(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr));
            }

            internal override void OnRedirectReceived(IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr, string newLocationUrl)
            {
                OnRedirectReceived(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), newLocationUrl);
            }

            internal override void OnResponseStarted(IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr)
            {
               OnResponseStarted(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr));
            }

            internal override void OnReadCompleted(IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr, IntPtr bufferPtr, ulong bytesRead)
            {
                OnReadCompleted(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), new ByteBuffer(bufferPtr));
            }

            internal override void OnSucceeded(IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr)
            {
                OnSucceeded(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr));
            }

            internal override void OnFailed(IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr, IntPtr errorPtr)
            {
                OnFailed(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), new CronetException(errorPtr));
            }

            /// <summary>
            /// Default callback implementation.
            ///
            /// Inherit from UrlRequest.Callback to create your own.
            /// </summary>
            public class Default : UrlRequest.Callback
            {
                public override void OnRedirectReceived(UrlRequest request, UrlResponseInfo info, string newLocationUrl)
                {
                    // You should call the request.followRedirect() method to continue
                    // processing the request.
                    request.FollowRedirect();
                }

                public override void OnResponseStarted(UrlRequest request, UrlResponseInfo info)
                {
                    // You should call the request.read() method before the request can be
                    // further processed. The following instruction provides a ByteBuffer object
                    // with a capacity of 102400 bytes to the read() method.
                    request.Read(ByteBuffer.Allocate(102400));
                }
                
                public override void OnReadCompleted(UrlRequest request, UrlResponseInfo info, ByteBuffer byteBuffer)
                {
                    // You should keep reading the request until there's no more data.
                    request.Read(ByteBuffer.Allocate(102400));
                }
                
                public override void OnSucceeded(UrlRequest request, UrlResponseInfo info)
                {
                    // let's do nothing here
                }
                
                public override void OnFailed(UrlRequest request, UrlResponseInfo info, CronetException error)
                {
                    // let's do nothing here
                }
            }
        }
        
    }
}