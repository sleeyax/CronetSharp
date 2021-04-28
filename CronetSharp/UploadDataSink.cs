using System;
using System.Linq;

namespace CronetSharp
{
    public class UploadDataSink : IDisposable
    {
        private readonly IntPtr _uploadDataSinkPtr;
        
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
        
        public UploadDataSink(IntPtr uploadDataSinkPtr)
        {
            _uploadDataSinkPtr = uploadDataSinkPtr;
        }

        public UploadDataSink()
        {
            if (OnReadSucceeded == default && OnReadError == default && OnRewindSucceeded == default && OnRewindError == default)
            {
                _uploadDataSinkPtr = Cronet.UploadDataSink.Cronet_UploadDataSink_Create();
                return;
            }
            
            Cronet.UploadDataSink.OnReadSucceededFunc onReadSucceededFunc = (uploadDataSinkPtr, bytesRead, isFinalChunk) => OnReadSucceeded(bytesRead, isFinalChunk);
            Cronet.UploadDataSink.OnReadErrorFunc onReadErrorFunc = (uploadDataSinkPtr, error) => OnReadError(new Exception(error));
            Cronet.UploadDataSink.OnRewindSucceededFunc onRewindSucceededFunc = uploadDataSinkPtr => OnRewindSucceeded();
            Cronet.UploadDataSink.OnRewindErrorFunc onRewindErrorFunc = (uploadDataSinkPtr, error) => OnRewindError(new Exception(error));
            
            var handles = new object[]{onReadSucceededFunc, onReadErrorFunc, onRewindSucceededFunc, onRewindErrorFunc}.Select(GCManager.Alloc);

            _uploadDataSinkPtr = Cronet.UploadDataSink.Cronet_UploadDataSink_CreateWith(
                onReadSucceededFunc,
                onReadErrorFunc,
                onRewindSucceededFunc,
                onRewindErrorFunc    
            );
            
            GCManager.Register(_uploadDataSinkPtr, handles.ToArray());
        }

        public void Dispose()
        {
            Cronet.UploadDataSink.Cronet_UploadDataSink_Destroy(_uploadDataSinkPtr);
            GCManager.Free(_uploadDataSinkPtr);
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