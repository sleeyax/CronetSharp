using System;

namespace CronetSharp
{
    public class Runnable : IDisposable
    {
        public IntPtr Pointer { get; }

        public Runnable(Action handler)
        {
            Pointer = Cronet.Runnable.Cronet_Runnable_CreateWith(_ => handler());
        }
        
        public Runnable(IntPtr pointer)
        {
            Pointer = pointer;
        }
        
        public void Dispose()
        {
            Cronet.Runnable.Cronet_Runnable_Destroy(Pointer);
        }

        public void Run()
        {
            Cronet.Runnable.Cronet_Runnable_Run(Pointer);
        }
    }
}