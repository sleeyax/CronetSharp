using System;

namespace CronetSharp
{
    public class UploadDataSink : IDisposable
    {
        private readonly IntPtr _uploadDataSinkPtr;
        
        public UploadDataSink(IntPtr uploadDataSinkPtr)
        {
            _uploadDataSinkPtr = uploadDataSinkPtr;
        }

        public UploadDataSink()
        {
            _uploadDataSinkPtr = Cronet.UploadDataSink.Cronet_UploadDataSink_Create();
        }

        public UploadDataSink(UploadDataSinkHandler handler)
        {
            _uploadDataSinkPtr = Cronet.UploadDataSink.Cronet_UploadDataSink_CreateWith(
                (uploadDataSinkPtr, bytesRead, isFinalChunk) => handler.OnReadSucceeded(bytesRead, isFinalChunk),
                (uploadDataSinkPtr, error) => handler.OnReadError(new Exception(error)),
                uploadDataSinkPtr => handler.OnRewindSucceeded(),
                (uploadDataSinkPtr, error) => handler.OnRewindError(new Exception(error))
            );
        }

        public void Dispose()
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_Destroy(_uploadDataSinkPtr);
        }

        /// <summary>
        /// Notify data sink listener that read succeeded
        /// </summary>
        /// <param name="bytesRead"></param>
        /// <param name="isFinalChunk"></param>
        internal void NotifyReadSucceeded(ulong bytesRead, bool isFinalChunk)
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_OnReadSucceeded(_uploadDataSinkPtr, bytesRead, isFinalChunk);
        }
        
        /// <summary>
        /// Notify data sink listener that read failed
        /// </summary>
        /// <param name="error"></param>
        internal void NotifyReadError(string error)
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_OnReadError(_uploadDataSinkPtr, error);
        }
        
        /// <summary>
        /// Notify data sink listener that rewind succeeded
        /// </summary>
        internal void NotifyRewindSucceeded()
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_OnRewindSucceeded(_uploadDataSinkPtr);
        }

        /// <summary>
        /// Notify data sink listener that rewind failed
        /// </summary>
        /// <param name="error"></param>
        internal void NotifyRewindSucceeded(string error)
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_OnRewindError(_uploadDataSinkPtr, error);
        }
    }
}