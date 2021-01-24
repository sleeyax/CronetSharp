using System;

namespace CronetSharp
{
    public class UrlRequestCallback : IDisposable
    {
        public IntPtr Pointer { get; }
        
        public UrlRequestCallback(UrlRequestCallbackHandler handler)
        {
            Pointer = Cronet.UrlRequestCallback.Cronet_UrlRequestCallback_CreateWith(
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr, newLocationUrl) => handler.OnRedirectReceived(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), newLocationUrl),
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr) => handler.OnResponseStarted(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr)),
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr, byteBufferPtr, bytesRead) => handler.OnReadCompleted(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), new ByteBuffer(byteBufferPtr), bytesRead),
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr) => handler.OnSucceeded(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr)),
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr, errorPtr) => handler.OnFailed(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), new CronetException(errorPtr)),
                (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr) => handler.OnCancelled(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr))
            );
        }

        public void Dispose()
        {
            Cronet.UrlRequestCallback.Cronet_UrlRequestCallback_Destroy(Pointer);
        }
    }
}