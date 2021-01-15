using System;
using System.Collections.Generic;

namespace CronetSharp
{
    public class UrlRequestParams
    {
        public IntPtr Pointer { get; }

        private Dictionary<string, string> _headers;

        public UrlRequestParams()
        {
            Pointer = Cronet.UrlRequestParams.Cronet_UrlRequestParams_Create();
        }
        
        public UrlRequestParams(IntPtr urlRequestPtr)
        {
            Pointer = urlRequestPtr;
        }

        public void AddHeader(string header, string value)
        {
            _headers[header] = value;
            IntPtr headersPtr = IntPtr.Zero; // TODO: create headers
            Cronet.UrlRequestParams.Cronet_UrlRequestParams_request_headers_add(Pointer, headersPtr);
        }

        /// <summary>
        /// Set the request headers.
        /// </summary>
        /// <param name="headers"></param>
        public void SetHeaders(Dictionary<string, string> headers)
        {
            _headers = headers;
            foreach (var header in headers)
                AddHeader(header.Key, header.Value);
        }
        
        /// <summary>
        /// Set or get the headers.
        /// </summary>
        public Dictionary<string, string> Headers
        {
            get => _headers;
            set => SetHeaders(value);
        }

        /// <summary>
        /// Marks that the executors this request will use to notify callbacks (for UploadDataProviders and UrlRequest.Callbacks) is intentionally performing inline execution.
        /// </summary>
        public bool AllowDirectExecutor
        {
            get => Cronet.UrlRequestParams.Cronet_UrlRequestParams_allow_direct_executor_get(Pointer);
            set => Cronet.UrlRequestParams.Cronet_UrlRequestParams_allow_direct_executor_set(Pointer, value);
        }

        /// <summary>
        /// Disables cache for the request.
        /// </summary>
        public bool DisableCache
        {
            get => Cronet.UrlRequestParams.Cronet_UrlRequestParams_disable_cache_get(Pointer);
            set => Cronet.UrlRequestParams.Cronet_UrlRequestParams_disable_cache_set(Pointer, value);
        }

        /// <summary>
        /// Sets the HTTP method verb to use for this request.
        /// The default when this method is not called is "GET" if the request has no body or "POST" if it does.
        /// Supported methods: "GET", "HEAD", "DELETE", "POST" or "PUT".
        /// </summary>
        public string HttpMethod
        {
            get => Cronet.UrlRequestParams.Cronet_UrlRequestParams_http_method_get(Pointer);
            set => Cronet.UrlRequestParams.Cronet_UrlRequestParams_http_method_set(Pointer, value);
        }

        /// <summary>
        /// Sets priority of the request.
        /// </summary>
        public Cronet.RequestPriority Priority
        {
            get => Cronet.UrlRequestParams.Cronet_UrlRequestParams_priority_get(Pointer);
            set => Cronet.UrlRequestParams.Cronet_UrlRequestParams_priority_set(Pointer, value);
        }
    }
}