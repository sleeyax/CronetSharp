using System;

namespace CronetSharp
{
    public class Executor : IDisposable
    {
        public IntPtr Pointer { get; }
        
        public Executor() : this(runnable =>
        {
            runnable.Run();
            runnable.Dispose();
        }) {}
        
        public Executor(Action<Runnable> hander)
        {
            Pointer = Cronet.Executor.Cronet_Executor_CreateWith((executorPtr, runnablePtr) => hander(new Runnable(runnablePtr)));
        }

        public Executor(IntPtr executorPtr)
        {
            Pointer = executorPtr;
        }

        public void Dispose()
        {
            Cronet.Executor.Cronet_Executor_Destroy(Pointer);
        }

        public void Execute(Runnable runnable)
        {
            Cronet.Executor.Cronet_Executor_Execute(Pointer, runnable.Pointer);
        }
    }
}