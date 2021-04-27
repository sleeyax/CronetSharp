using System;

namespace CronetSharp
{
    public class RequestFinishedInfoListener : IDisposable
    {
        public IntPtr Pointer { get; }

        public RequestFinishedInfoListener(Action<RequestFinishedInfo, UrlResponseInfo, CronetException> handler)
        {
            Cronet.RequestFinishedInfoListener.OnRequestFinishedFunc onRequestFinishedFunc = (requestFinishedInfoListenerPtr, requestFinishedInfoPtr, urlResponseInfoPtr, errorPtr) => handler(new RequestFinishedInfo(requestFinishedInfoPtr), new UrlResponseInfo(urlResponseInfoPtr), new CronetException(errorPtr));
            var handle = GCManager.Alloc(onRequestFinishedFunc);
            Pointer = Cronet.RequestFinishedInfoListener.Cronet_RequestFinishedInfoListener_CreateWith(onRequestFinishedFunc);
            GCManager.Register(Pointer, handle);
        }

        public RequestFinishedInfoListener(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public void Dispose()
        {
            Cronet.RequestFinishedInfoListener.Cronet_RequestFinishedInfoListener_Destroy(Pointer);
            GCManager.Free(Pointer);
        }
    }
}