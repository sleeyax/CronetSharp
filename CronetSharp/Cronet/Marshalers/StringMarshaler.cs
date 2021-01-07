using System;
using System.Runtime.InteropServices;

namespace CronetSharp.Cronet.Marshalers
{
    public class StringMarshaler : ICustomMarshaler
    {
        public void CleanUpManagedData(object managedObj) {}

        public void CleanUpNativeData(IntPtr pNativeData) => pNativeData = IntPtr.Zero;

        public int GetNativeDataSize() => throw new NotSupportedException();

        public IntPtr MarshalManagedToNative(object managedObj) => !(managedObj is string) ? IntPtr.Zero : Marshal.StringToCoTaskMemAnsi((string) managedObj);

        public object MarshalNativeToManaged(IntPtr pNativeData) => Marshal.PtrToStringAnsi(pNativeData);

        public static ICustomMarshaler GetInstance(string cookie) => new StringMarshaler();
    }
}