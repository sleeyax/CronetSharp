namespace CronetSharp.Server
{
    /// <summary>
    /// Represents a HTTP response from a Cronet Engine.
    /// </summary>
    internal class CronetResponse
    {
        /// <summary>
        /// Request ID that this response belongs to.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Optional error that occured.
        /// If this is not null, all other fields of this response are most likely null.
        /// </summary>
        public string Error { get; set; }
        
        /// <summary>
        /// HTTP status code
        /// </summary>
        public int StatusCode { get; set; }
        
        /// <summary>
        /// HTTP status code text.
        /// </summary>
        public string StatusCodeText { get; set; }
        
        /// <summary>
        /// Response headers.
        /// </summary>
        public HttpHeader[] Headers { get; set; }
        
        /// <summary>
        /// Response body.
        /// </summary>
        public string Body { get; set; }
        
        /// <summary>
        /// Raw response body.
        /// </summary>
        public byte[] BodyRaw { get; set; }
    }
}