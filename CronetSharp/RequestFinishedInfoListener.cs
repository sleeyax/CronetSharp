using System;

namespace CronetSharp
{
    public class RequestFinishedInfoListener : IDisposable
    {
        public IntPtr Pointer { get; }

        public RequestFinishedInfoListener(Action<RequestFinishedInfo, UrlResponseInfo, CronetException> handler)
        {
            Pointer = Cronet.RequestFinishedInfoListener.Cronet_RequestFinishedInfoListener_CreateWith(
                (requestFinishedInfoListenerPtr, requestFinishedInfoPtr, urlResponseInfoPtr, errorPtr) => handler(new RequestFinishedInfo(requestFinishedInfoPtr), new UrlResponseInfo(urlResponseInfoPtr), new CronetException(errorPtr))
            );
        }

        public RequestFinishedInfoListener(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public void Dispose()
        {
            Cronet.RequestFinishedInfoListener.Cronet_RequestFinishedInfoListener_Destroy(Pointer);
        }
    }
}