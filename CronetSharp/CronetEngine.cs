using System;
using CronetSharp.Wrapper;

namespace CronetSharp
{
    public class CronetEngine
    {
        private readonly IntPtr _enginePtr;
        
        public CronetEngine()
        {
            _enginePtr = Cronet.Engine.Cronet_Engine_Create();
            Console.WriteLine(_enginePtr);
        }
    }
}