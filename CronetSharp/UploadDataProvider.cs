using System;
using System.Linq;
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

        /// <summary>
        /// If this is a non-chunked upload, returns the length of the upload.
        /// </summary>
        public Func<long> OnGetLength;

        /// <summary>
        /// Reads upload data into byteBuffer.
        /// </summary>
        public Action<UploadDataSink, ByteBuffer> OnRead;

        /// <summary>
        /// Rewinds upload data.
        ///
        /// This is mostly useful for chunked uploads.
        /// E.g callback code resets index value to 0 in order to prepare for the next stream of uploaded chunks.
        /// </summary>
        public Action<UploadDataSink> OnRewind;

        /// <summary>
        /// Called when this UploadDataProvider is no longer needed by a request, so that resources (like a file) can be explicitly released.
        /// </summary>
        public Action OnClose;

        public UploadDataProvider()
        {
            Cronet.UploadDataProvider.GetLengthFunc getLengthFunc = uploadDataProviderPtr => OnGetLength();
            Cronet.UploadDataProvider.ReadFunc readFunc = (uploadDataProviderPtr, uploadDataSinkPtr, byteBufferPtr) => OnRead(new UploadDataSink(uploadDataSinkPtr), new ByteBuffer(byteBufferPtr));
            Cronet.UploadDataProvider.RewindFunc rewindFunc = (uploadDataProviderPtr, uploadDataSinkPtr) => OnRewind(new UploadDataSink(uploadDataSinkPtr));
            Cronet.UploadDataProvider.CloseFunc closeFunc = uploadDataProviderPtr => OnClose();

            var handles = new object[] {getLengthFunc, readFunc, rewindFunc, closeFunc}.Select(GCManager.Alloc);

            Pointer = Cronet.UploadDataProvider.Cronet_UploadDataProvider_CreateWith(
                getLengthFunc,
                readFunc,
                rewindFunc,
                closeFunc
            );

            GCManager.Register(Pointer, handles.ToArray());
        }

        public UploadDataProvider(IntPtr uploadDataProviderPtr)
        {
            Pointer = uploadDataProviderPtr;
        }

        public void Dispose()
        {
            if (Pointer == IntPtr.Zero)
            {
                return;
            }

            Cronet.UploadDataProvider.Cronet_UploadDataProvider_Destroy(Pointer);
            GCManager.Free(Pointer);
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
            return new UploadDataProvider {
                OnRead = (uploadDataSink, byteBuffer) =>
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
                OnGetLength = () => (long) length,
                OnRewind = _ => { },
                OnClose = () => { }
            };;
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

        [Obsolete(Constants.MethodIsTestOnly)]
        public void Read(UploadDataSink uploadDataSink, ByteBuffer byteBuffer)
        {
            Cronet.UploadDataProvider.Cronet_UploadDataProvider_Read(Pointer, uploadDataSink.Pointer, byteBuffer.Pointer);
        }

        [Obsolete(Constants.MethodIsTestOnly)]
        public long GetLength()
        {
            return Cronet.UploadDataProvider.Cronet_UploadDataProvider_GetLength(Pointer);
        }

        [Obsolete(Constants.MethodIsTestOnly)]
        public void Rewind(UploadDataSink uploadDataSink)
        {
            Cronet.UploadDataProvider.Cronet_UploadDataProvider_Rewind(Pointer, uploadDataSink.Pointer);
        }
    }
}