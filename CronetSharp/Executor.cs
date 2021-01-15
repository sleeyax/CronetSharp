using System;
using CronetSharp.Cronet;

namespace CronetSharp
{
    public class Executor : IExecutor
    {
        /// <summary>
        /// Executes a callable synchronously.
        ///
        /// Inherit from this class to create your own implementation.
        /// </summary>
        public Executor() {}
        
        protected void Run(IntPtr runnablePtr) =>  Runnable.Cronet_Runnable_Run(runnablePtr);
        
        protected void Destroy(IntPtr runnablePtr) =>  Runnable.Cronet_Runnable_Destroy(runnablePtr);

        public virtual void Execute(IntPtr runnablePtr)
        {
            Run(runnablePtr);
            Destroy(runnablePtr);
        }
    }
}