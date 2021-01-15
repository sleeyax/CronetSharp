using System;

namespace CronetSharp
{
    public class CronetException : Exception
    {
        private readonly IntPtr _errorPtr;
        
        public CronetException()
        {
            _errorPtr = Cronet.Error.Cronet_Error_Create();
        }
        
        public CronetException(IntPtr errorPtr)
        {
            _errorPtr = errorPtr;
        }

        public void Destroy()
        {
            Cronet.Error.Cronet_Error_Destroy(_errorPtr);    
        }

        public Cronet.ErrorCode ErrorCode
        {
            get => Cronet.Error.Cronet_Error_error_code_get(_errorPtr);
            set => Cronet.Error.Cronet_Error_error_code_set(_errorPtr, value);
        }
        
        public int InternalErrorCode
        {
            get => Cronet.Error.Cronet_Error_internal_error_code_get(_errorPtr);
            set => Cronet.Error.Cronet_Error_internal_error_code_set(_errorPtr, value);
        }

        public string CronetErrorMessage
        {
            get => Cronet.Error.Cronet_Error_message_get(_errorPtr);
            set => Cronet.Error.Cronet_Error_message_set(_errorPtr, value);
        }
        
        public bool ImmediatelyRetryable
        {
            get => Cronet.Error.Cronet_Error_immediately_retryable_get(_errorPtr);
            set => Cronet.Error.Cronet_Error_immediately_retryable_set(_errorPtr, value);
        }
        
        public int QuicDetailedErrorCode
        {
            get => Cronet.Error.Cronet_Error_quic_detailed_error_code_get(_errorPtr);
            set => Cronet.Error.Cronet_Error_quic_detailed_error_code_set(_errorPtr, value);
        }
        
    }
}