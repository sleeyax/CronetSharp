using System;

namespace CronetSharp.Cronet
{
    public interface IExecutor
    {
        void Execute(IntPtr runnablePtr);
    }
}