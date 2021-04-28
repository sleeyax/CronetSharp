using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace CronetSharp
{
    /// <summary>
    /// Helper class that helps with Garbage Collection Management of managed delegates to unmanaged code.
    /// </summary>
    public static class GCManager
    {
        private static ConcurrentDictionary<IntPtr, GCHandle[]> _registeredHandles = new ConcurrentDictionary<IntPtr, GCHandle[]>();
        
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
            _registeredHandles.TryAdd(ptr, handles);
        }

        /// <summary>
        /// Releases a GCHandle.
        /// </summary>
        /// <param name="ptr"></param>
        public static void Free(IntPtr ptr)
        {
            if (ptr == default) return;
            
            var registeredHandle = _registeredHandles.FirstOrDefault(p => p.Key == ptr);
            
            if (registeredHandle.Key == default) return;

            foreach (var handle in registeredHandle.Value)
                if (handle.IsAllocated) handle.Free();
            
            _registeredHandles.TryRemove(ptr, out _);
        }
    }
}