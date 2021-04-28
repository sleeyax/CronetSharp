using System;
using System.Linq;

namespace CronetSharp
{
    public class UploadDataSink : IDisposable
    {
        public IntPtr Pointer { get; }
        
        /// <summary>
        /// Called by UploadDataProvider when a read succeeds.
        /// </summary>
        public Action<ulong, bool> OnReadSucceeded;
        
        /// <summary>
        /// Called by UploadDataProvider when a read fails.
        /// </summary>
        public Action<Exception> OnReadError;
        
        /// <summary>
        /// Called by UploadDataProvider when a rewind succeeds.
        /// </summary>
        public Action OnRewindSucceeded;
        
        /// <summary>
        /// Called by UploadDataProvider when a rewind fails, or if rewinding uploads is not supported.
        /// </summary>
        public Action<Exception> OnRewindError;
        
        public UploadDataSink(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public UploadDataSink()
        {
            Cronet.UploadDataSink.OnReadSucceededFunc onReadSucceededFunc = (uploadDataSinkPtr, bytesRead, isFinalChunk) => OnReadSucceeded(bytesRead, isFinalChunk);
            Cronet.UploadDataSink.OnReadErrorFunc onReadErrorFunc = (uploadDataSinkPtr, error) => OnReadError(new Exception(error));
            Cronet.UploadDataSink.OnRewindSucceededFunc onRewindSucceededFunc = uploadDataSinkPtr => OnRewindSucceeded();
            Cronet.UploadDataSink.OnRewindErrorFunc onRewindErrorFunc = (uploadDataSinkPtr, error) => OnRewindError(new Exception(error));
            
            var handles = new object[]{onReadSucceededFunc, onReadErrorFunc, onRewindSucceededFunc, onRewindErrorFunc}.Select(GCManager.Alloc);

            Pointer = Cronet.UploadDataSink.Cronet_UploadDataSink_CreateWith(
                onReadSucceededFunc,
                onReadErrorFunc,
                onRewindSucceededFunc,
                onRewindErrorFunc    
            );
            
            GCManager.Register(Pointer, handles.ToArray());
        }

        public void Dispose()
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_Destroy(Pointer);
            GCManager.Free(Pointer);
        }

        /// <summary>
        /// Notify data sink listener that read succeeded
        /// </summary>
        /// <param name="bytesRead"></param>
        /// <param name="isFinalChunk"></param>
        public void NotifyReadSucceeded(ulong bytesRead, bool isFinalChunk)
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_OnReadSucceeded(Pointer, bytesRead, isFinalChunk);
        }
        
        /// <summary>
        /// Notify data sink listener that read failed
        /// </summary>
        /// <param name="error"></param>
        public void NotifyReadError(string error)
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_OnReadError(Pointer, error);
        }
        
        /// <summary>
        /// Notify data sink listener that rewind succeeded
        /// </summary>
        public void NotifyRewindSucceeded()
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_OnRewindSucceeded(Pointer);
        }

        /// <summary>
        /// Notify data sink listener that rewind failed
        /// </summary>
        /// <param name="error"></param>
        public void NotifyRewindSucceeded(string error)
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_OnRewindError(Pointer, error);
        }
    }
}