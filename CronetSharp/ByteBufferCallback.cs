using System;

namespace CronetSharp
{
    public class ByteBufferCallback
    {
        public IntPtr Pointer { get; }
        
        public ByteBufferCallback(Action<ByteBuffer> onDestroy)
        {
            Pointer = Cronet.BufferCallback.Cronet_BufferCallback_CreateWith((bufferCallbackPtr, bufferPtr) => onDestroy(new ByteBuffer(bufferPtr)));
        }

        public ByteBufferCallback(IntPtr byteBufferCallbackPtr)
        {
            Pointer = byteBufferCallbackPtr;
        }

        public void Destroy()
        {
            Cronet.BufferCallback.Cronet_BufferCallback_Destroy(Pointer);
        }
    }
}