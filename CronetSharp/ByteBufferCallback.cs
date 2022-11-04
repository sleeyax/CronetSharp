using System;
using System.Runtime.InteropServices;

namespace CronetSharp
{
    public class ByteBufferCallback : IDisposable
    {
        public IntPtr Pointer { get; }

        public ByteBufferCallback(Action<ByteBuffer> onDestroy)
        {
            Cronet.BufferCallback.OnDestroyFunc onDestroyFunc = (bufferCallbackPtr, bufferPtr) => onDestroy(new ByteBuffer(bufferPtr));
            var handle = GCManager.Alloc(onDestroyFunc);
            Pointer = Cronet.BufferCallback.Cronet_BufferCallback_CreateWith(onDestroyFunc);
            GCManager.Register(Pointer, handle);
        }

        public ByteBufferCallback(IntPtr byteBufferCallbackPtr)
        {
            Pointer = byteBufferCallbackPtr;
        }

        public void Dispose()
        {
            if (Pointer == IntPtr.Zero)
            {
                return;
            }

            Cronet.BufferCallback.Cronet_BufferCallback_Destroy(Pointer);
            GCManager.Free(Pointer);
        }
    }
}