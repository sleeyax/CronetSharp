using System;
using CronetSharp.Cronet;

namespace CronetSharp
{
    public class RequestFinishedInfo : IDisposable
    {
        private readonly IntPtr _requestFinishedInfoPtr;

        public RequestFinishedInfo()
        {
            _requestFinishedInfoPtr = Cronet.RequestFinishedInfo.Cronet_RequestFinishedInfo_Create();
        }

        public RequestFinishedInfo(IntPtr requestFinishedInfoPtr)
        {
            _requestFinishedInfoPtr = requestFinishedInfoPtr;
        }

        public void Dispose()
        {
            Cronet.RequestFinishedInfo.Cronet_RequestFinishedInfo_Destroy(_requestFinishedInfoPtr);
        }

        /// <summary>
        /// Reason to why this request was finished.
        /// </summary>
        public RequestFinishedReason Reason
        {
            get => Cronet.RequestFinishedInfo.Cronet_RequestFinishedInfo_finished_reason_get(_requestFinishedInfoPtr);
            set => Cronet.RequestFinishedInfo.Cronet_RequestFinishedInfo_finished_reason_set(_requestFinishedInfoPtr, value);
        }
    }
}