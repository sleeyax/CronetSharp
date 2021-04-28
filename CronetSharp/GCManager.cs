using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CronetSharp
{
    /// <summary>
    /// Helper class that helps with Garbage Collection Management of managed delegates to unmanaged code.
    /// </summary>
    public static class GCManager
    {
        private static Dictionary<IntPtr, GCHandle[]> _registeredHandles = new Dictionary<IntPtr, GCHandle[]>();
        
        /// <summary>
        /// Allocates a handle with recommended type for the specified object.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GCHandle Alloc(object value) => GCHandle.Alloc(value);

        /// <summary>
        /// Registers handles for the specified IntPtr.
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="handles"></param>
        public static void Register(IntPtr ptr, params GCHandle[] handles)
        {
            _registeredHandles.Add(ptr, handles);
        }

        /// <summary>
        /// Releases a GCHandle.
        /// </summary>
        /// <param name="ptr"></param>
        public static void Free(IntPtr ptr)
        {
            if (ptr == default) return;
            
            var handlePtr = _registeredHandles.Keys.ToArray().FirstOrDefault(p => p == ptr);
            
            if (handlePtr == default) return;

            var handles = _registeredHandles[handlePtr];
            
            foreach (var handle in handles)
                if (handle.IsAllocated) handle.Free();
            
            _registeredHandles.Remove(ptr);
        }
    }
}