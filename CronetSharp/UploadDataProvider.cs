using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CronetSharp
{
    /// <summary>
    /// Class allowing the embedder to provide an upload body to UrlRequest.
    /// It supports both non-chunked (size known in advanced) and chunked (size not known in advance) uploads.
    /// Be aware that not all servers support chunked uploads. 
    /// </summary>
    public class UploadDataProvider : IDisposable
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

        public UploadDataProvider(IntPtr uploadDataProviderPtr)
        {
            Pointer = uploadDataProviderPtr;
        }

        public void Dispose()
        {
            Cronet.UploadDataProvider.Cronet_UploadDataProvider_Destroy(Pointer);
        }

        public void Close()
        {
            Cronet.UploadDataProvider.Cronet_UploadDataProvider_Close(Pointer);
        }

        /// <summary>
        /// Uploads length of bytes from data, starting from offset
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="isFinalChunk">For chunked uploads, true if this is the final read. It must be false for non-chunked uploads.</param>
        /// <returns></returns>
        public static UploadDataProvider Create(byte[] data, int offset, int length, bool isFinalChunk = false)
        {
            var handler = new UploadDataProviderHandler
            {
                Read = (uploadDataSink, byteBuffer) =>
                {
                    try
                    {
                        var dest = Cronet.Buffer.Cronet_Buffer_GetData(byteBuffer.Pointer);
                        Marshal.Copy(data, offset, dest, length);
                        uploadDataSink.NotifyReadSucceeded((ulong) length, isFinalChunk);
                    }
                    catch (Exception ex)
                    {
                        uploadDataSink.NotifyReadError(ex.Message);
                    }
                },
                GetLength = () => (long) length,
                Rewind = _ => { },
                Close = () => { }
            };

            return new UploadDataProvider(handler);
        }

        /// <summary>
        /// Uploads all data bytes, from beginning to end.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static UploadDataProvider Create(byte[] data)
        {
            return Create(data, 0, data.Length);
        }
        
        /// <summary>
        /// Uploads string of data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static UploadDataProvider Create(string data)
        {
            return Create(Encoding.ASCII.GetBytes(data));
        }
    }
}