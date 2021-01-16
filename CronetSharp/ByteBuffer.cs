using System;

namespace CronetSharp
{
    public class ByteBuffer
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
        public void Destroy()
        {
            Cronet.Buffer.Cronet_Buffer_Destroy(Pointer);
        }

        /// <summary>
        /// Read buffer contents.
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            return Cronet.Buffer.Cronet_Buffer_GetData(Pointer);
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