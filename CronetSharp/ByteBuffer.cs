using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Marshalers;

namespace CronetSharp
{
    public class ByteBuffer : IDisposable
    {
        public IntPtr Pointer { get; }

        public ByteBuffer()
        {
            Pointer = Cronet.Buffer.Cronet_Buffer_Create();
        }
        
        public ByteBuffer(IntPtr bufferPtr)
        {
            Pointer = bufferPtr;
        }
        
        public ByteBuffer(ulong size)
        {
            Pointer = Cronet.Buffer.Cronet_Buffer_Create();
            Cronet.Buffer.Cronet_Buffer_InitWithAlloc(Pointer, size);
        }

        public ByteBuffer(ulong size, byte[] data, ByteBufferCallback callback)
        {
            Pointer = Cronet.Buffer.Cronet_Buffer_Create();
            IntPtr rawDataPtr = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, rawDataPtr, data.Length);
            Cronet.Buffer.Cronet_Buffer_InitWithDataAndCallback(Pointer, rawDataPtr, size, callback.Pointer);
        }

        public ByteBuffer(ulong size, byte[] data) : this(size, data, new ByteBufferCallback(_ => { })) { }

        /// <summary>
        /// Allocates a new byte buffer. 
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public static ByteBuffer Allocate(ulong capacity)
        {
            return new ByteBuffer(capacity);
        }

        /// <summary>
        /// Destroy this buffer and free up memory.
        /// </summary>
        public void Dispose()
        {
            Cronet.Buffer.Cronet_Buffer_Destroy(Pointer);
        }

        /// <summary>
        /// Clear the buffer's contents.
        ///
        /// This method won't destroy the buffer.
        /// </summary>
        public void Clear()
        {
            IntPtr dest = Cronet.Buffer.Cronet_Buffer_GetData(Pointer);
            int size = (int) Cronet.Buffer.Cronet_Buffer_GetSize(Pointer);
            Marshal.Copy(new byte[size], 0, dest, size);
        }

        /// <summary>
        /// Read buffer contents as string.
        /// </summary>
        /// <returns></returns>
        public string GetDataAsString()
        {
            return (string) new StringMarshaler().MarshalNativeToManaged(Cronet.Buffer.Cronet_Buffer_GetData(Pointer));
        }
        
        /// <summary>
        /// Read buffer contents.
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            int length = (int) Cronet.Buffer.Cronet_Buffer_GetSize(Pointer);
            byte[] dest = new byte[length];
            var src = Cronet.Buffer.Cronet_Buffer_GetData(Pointer);
            Marshal.Copy(src, dest, 0, length);
            return dest;
        }

        /// <summary>
        /// Returns the size of the buffer.
        /// </summary>
        /// <returns></returns>
        public ulong GetSize()
        {
            return Cronet.Buffer.Cronet_Buffer_GetSize(Pointer);
        }
    }
}