using System;
using System.Runtime.InteropServices;

namespace CronetSharp.Cronet.Marshalers
{
    public class StringMarshaler : ICustomMarshaler
    {
        public void CleanUpManagedData(object managedObj) {}
        
        public void CleanUpNativeData(IntPtr pNativeData) {}
        
        public int GetNativeDataSize() => throw new NotSupportedException();
        
        public IntPtr MarshalManagedToNative(object managedObj) => throw new NotSupportedException();
        
        public object MarshalNativeToManaged(IntPtr pNativeData) => Marshal.PtrToStringAnsi(pNativeData);
        
        public static ICustomMarshaler GetInstance(string cookie) => new StringMarshaler();
    }
}