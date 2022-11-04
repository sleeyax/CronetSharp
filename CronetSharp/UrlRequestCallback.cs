using System;
using System.Linq;

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
            Cronet.UrlRequestCallback.OnRedirectReceivedFunc onRedirectReceivedFunc = (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr, newLocationUrl) => OnRedirectReceived(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), newLocationUrl);
            Cronet.UrlRequestCallback.OnResponseStartedFunc onResponseStartedFunc= (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr) => OnResponseStarted(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr));
            Cronet.UrlRequestCallback.OnReadCompletedFunc onReadCompletedFunc= (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr, byteBufferPtr, bytesRead) => OnReadCompleted(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), new ByteBuffer(byteBufferPtr), bytesRead);
            Cronet.UrlRequestCallback.OnSucceededFunc onSucceededFunc = (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr) => OnSucceeded(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr));
            Cronet.UrlRequestCallback.OnFailedFunc onFailedFunc = (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr, errorPtr) => OnFailed(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr), new CronetException(errorPtr));
            Cronet.UrlRequestCallback.OnCanceledFunc onCanceledFunc = (urlRequestCallbackPtr, urlRequestPtr, urlResponseInfoPtr) => OnCancelled(new UrlRequest(urlRequestPtr), new UrlResponseInfo(urlResponseInfoPtr));

            var handles = new object[]
            {
                onRedirectReceivedFunc,
                onResponseStartedFunc,
                onReadCompletedFunc,
                onSucceededFunc,
                onFailedFunc,
                onCanceledFunc
            }.Select(GCManager.Alloc);

            Pointer = Cronet.UrlRequestCallback.Cronet_UrlRequestCallback_CreateWith(
                onRedirectReceivedFunc,
                onResponseStartedFunc,
                onReadCompletedFunc,
                onSucceededFunc,
                onFailedFunc,
                onCanceledFunc
            );

            GCManager.Register(Pointer, handles.ToArray());
        }

        public void Dispose()
        {
            if (Pointer == IntPtr.Zero)
            {
                return;
            }

            Cronet.UrlRequestCallback.Cronet_UrlRequestCallback_Destroy(Pointer);
            GCManager.Free(Pointer);
        }
    }
}