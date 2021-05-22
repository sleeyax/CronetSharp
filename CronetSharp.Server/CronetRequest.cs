using CronetSharp.Cronet;

namespace CronetSharp.Server
{
    internal class SlimUrlRequestParameters
    {
        public bool DisableCache { get; set; }
        
        public RequestPriority RequestPriority { get; set; }
    }
    
    /// <summary>
    /// Represents a HTTP request to fulfil using a Cronet Engine.
    /// </summary>
    internal class CronetRequest
    {
        /// <summary>
        /// Unique identifier for this request.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// HTTP method.
        /// Supports GET, POST, PUT, PATCH, DELETE.
        /// </summary>
        public string Method { get; set; }
        
        /// <summary>
        /// Target URL.
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// Headers to send along with the request.
        /// Not case sensitive.
        /// </summary>
        public HttpHeader[] Headers { get; set; }
        
        /// <summary>
        /// Optional HTTP body to send.
        /// </summary>
        public string Body { get; set; }
        
        /// <summary>
        /// Whether or not to follow redirects when the HTTP response status code is 3xx.
        /// </summary>
        public bool FollowRedirects { get; set; }

        /// <summary>
        /// Engine parameters to use for this request.
        /// </summary>
        public CronetEngineParams EngineParams { get; set; }
        
        /// <summary>
        /// Configure UrlRequest parameters for this request.
        /// </summary>
        public SlimUrlRequestParameters UrlRequestParameters { get; set; }
    }
}