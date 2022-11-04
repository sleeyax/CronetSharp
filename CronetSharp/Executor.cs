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
            Cronet.Executor.ExecuteFunc executeFunc = (executorPtr, runnablePtr) => hander(new Runnable(runnablePtr));
            var handle = GCManager.Alloc(executeFunc);
            Pointer = Cronet.Executor.Cronet_Executor_CreateWith(executeFunc);
            GCManager.Register(Pointer, handle);
        }

        public Executor(IntPtr executorPtr)
        {
            Pointer = executorPtr;
        }

        public void Dispose()
        {
            if (Pointer == IntPtr.Zero)
            {
                return;
            }

            Cronet.Executor.Cronet_Executor_Destroy(Pointer);
            GCManager.Free(Pointer);
        }

        public void Execute(Runnable runnable)
        {
            Cronet.Executor.Cronet_Executor_Execute(Pointer, runnable.Pointer);
        }
    }
}