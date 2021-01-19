using System;

namespace CronetSharp
{
    public class UploadDataSinkHandler
    {
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
    }
}