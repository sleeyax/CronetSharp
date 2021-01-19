using System;

namespace CronetSharp
{
    public class UrlRequestCallbackHandler
    {
        /// <summary>
        /// Invoked whenever a redirect is encountered.
        /// </summary>
        public Action<UrlRequest, UrlResponseInfo, string> OnRedirectReceived;
        /// <summary>
        /// Invoked when the final set of headers, after all redirects, is received.
        /// </summary>
        public Action<UrlRequest, UrlResponseInfo> OnResponseStarted;
        /// <summary>
        /// Invoked whenever part of the response body has been read.
        /// </summary>
        public Action<UrlRequest, UrlResponseInfo, ByteBuffer, ulong> OnReadCompleted;
        /// <summary>
        /// Invoked when request is completed successfully.
        /// </summary>
        public Action<UrlRequest, UrlResponseInfo> OnSucceeded;
        /// <summary>
        /// Invoked if request failed for any reason after start().
        /// </summary>
        public Action<UrlRequest, UrlResponseInfo, CronetException> OnFailed;
        /// <summary>
        /// Invoked if request was canceled via cancel().
        /// </summary>
        public Action<UrlRequest, UrlResponseInfo> OnCancelled;
    }
}