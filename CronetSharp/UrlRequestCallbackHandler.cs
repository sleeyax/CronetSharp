using System;

namespace CronetSharp
{
    public class UrlRequestCallbackHandler
    {
        public Action<UrlRequest, UrlResponseInfo, string> OnRedirectReceived;
        public Action<UrlRequest, UrlResponseInfo> OnResponseStarted;
        public Action<UrlRequest, UrlResponseInfo, ByteBuffer, ulong> OnReadCompleted;
        public Action<UrlRequest, UrlResponseInfo> OnSucceeded;
        public Action<UrlRequest, UrlResponseInfo, CronetException> OnFailed;
        public Action<UrlRequest, UrlResponseInfo> OnCancelled;
    }
}