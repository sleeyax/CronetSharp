using System;

namespace CronetSharp
{
    public class UploadDataProviderHandler
    {
        /// <summary>
        /// If this is a non-chunked upload, returns the length of the upload.
        /// </summary>
        public Func<long> GetLength;
        /// <summary>
        /// Reads upload data into byteBuffer.
        /// </summary>
        public Action<UploadDataSink, ByteBuffer> Read;
        /// <summary>
        /// Rewinds upload data.
        ///
        /// This is mostly useful for chunked uploads.
        /// E.g callback code resets index value to 0 in order to prepare for the next stream of uploaded chunks. 
        /// </summary>
        public Action<UploadDataSink> Rewind;
        /// <summary>
        /// Called when this UploadDataProvider is no longer needed by a request, so that resources (like a file) can be explicitly released.
        /// </summary>
        public Action Close;
    }
}