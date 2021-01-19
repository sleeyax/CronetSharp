using System;

namespace CronetSharp
{
    public class UploadDataSink
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
    }
}