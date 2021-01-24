using System;

namespace CronetSharp
{
    public class RequestFinishedInfoListener : IDisposable
    {
        private readonly IntPtr _requestFinishedInfoListener;

        public RequestFinishedInfoListener(Action<RequestFinishedInfo, UrlResponseInfo, CronetException> handler)
        {
            _requestFinishedInfoListener = Cronet.RequestFinishedInfoListener.Cronet_RequestFinishedInfoListener_CreateWith(
                (requestFinishedInfoListenerPtr, requestFinishedInfoPtr, urlResponseInfoPtr, errorPtr) => handler(new RequestFinishedInfo(requestFinishedInfoPtr), new UrlResponseInfo(urlResponseInfoPtr), new CronetException(errorPtr))
            );
        }

        public void Dispose()
        {
            Cronet.RequestFinishedInfoListener.Cronet_RequestFinishedInfoListener_Destroy(_requestFinishedInfoListener);
        }
    }
}