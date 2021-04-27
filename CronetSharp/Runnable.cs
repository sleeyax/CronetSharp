using System;

namespace CronetSharp
{
    public class Runnable : IDisposable
    {
        public IntPtr Pointer { get; }

        public Runnable(Action handler)
        {
            Cronet.Runnable.RunFunc runFunc = _ => handler();
            var handle = GCManager.Alloc(runFunc);
            Pointer = Cronet.Runnable.Cronet_Runnable_CreateWith(runFunc);
            GCManager.Register(Pointer, handle);
        }
        
        public Runnable(IntPtr pointer)
        {
            Pointer = pointer;
        }
        
        public void Dispose()
        {
            Cronet.Runnable.Cronet_Runnable_Destroy(Pointer);
            GCManager.Free(Pointer);
        }

        public void Run()
        {
            Cronet.Runnable.Cronet_Runnable_Run(Pointer);
        }
    }
}