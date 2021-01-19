using System;

namespace CronetSharp
{
    /// <summary>
    /// Class allowing the embedder to provide an upload body to UrlRequest.
    /// It supports both non-chunked (size known in advanced) and chunked (size not known in advance) uploads.
    /// Be aware that not all servers support chunked uploads. 
    /// </summary>
    public class UploadDataProvider
    {
        public IntPtr Pointer { get; }
        
        public UploadDataProvider(UploadDataProviderHandler handler)
        {
            Pointer = Cronet.UploadDataProvider.Cronet_UploadDataProvider_CreateWith(
                uploadDataProviderPtr => handler.GetLength(),
                (uploadDataProviderPtr, uploadDataSinkPtr, byteBufferPtr) => handler.Read(new UploadDataSink(uploadDataSinkPtr), new ByteBuffer(byteBufferPtr)),
                (uploadDataProviderPtr, uploadDataSinkPtr) => handler.Rewind(new UploadDataSink(uploadDataSinkPtr)),
                uploadDataProviderPtr => handler.Close()
            );
        }
    }
}