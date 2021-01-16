using System;

namespace CronetSharp
{
    public class Executor
    {
        public IntPtr Pointer { get; }
        
        public Executor()
        {
            Pointer = Cronet.Executor.Cronet_Executor_CreateWith((self, command) =>
            {
                Cronet.Runnable.Cronet_Runnable_Run(command);
                Cronet.Runnable.Cronet_Runnable_Destroy(command);
            });
        }

        public void Destroy()
        {
            Cronet.Executor.Cronet_Executor_Destroy(Pointer);
        }
    }
}