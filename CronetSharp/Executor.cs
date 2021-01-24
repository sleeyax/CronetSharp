using System;

namespace CronetSharp
{
    public class Executor : IDisposable
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

        public Executor(IntPtr executorPtr)
        {
            Pointer = executorPtr;
        }

        public void Dispose()
        {
            Cronet.Executor.Cronet_Executor_Destroy(Pointer);
        }
    }
}