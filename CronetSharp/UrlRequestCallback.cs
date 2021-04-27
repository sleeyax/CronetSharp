using System;

namespace CronetSharp
{
    public class UrlRequestCallback : IDisposable
    {
        public IntPtr Pointer { get; }
        
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
        
        public UrlRequestCallback()
        {
            Pointer = Cronet.UrlRequestCallback.Cronet_UrlRequestCallback_CreateWith(
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr, newLocationUrl) => OnRedirectReceived(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), newLocationUrl),
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr) => OnResponseStarted(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr)),
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr, byteBufferPtr, bytesRead) => OnReadCompleted(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), new ByteBuffer(byteBufferPtr), bytesRead),
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr) => OnSucceeded(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr)),
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr, errorPtr) => OnFailed(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), new CronetException(errorPtr)),
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr) => OnCancelled(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr))
            );
        }

        public void Dispose()
        {
            Cronet.UrlRequestCallback.Cronet_UrlRequestCallback_Destroy(Pointer);
        }
    }
}