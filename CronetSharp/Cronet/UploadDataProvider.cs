using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Bin;

namespace CronetSharp.Cronet
{
    internal static class UploadDataProvider
    {
        internal delegate long GetLengthFunc(IntPtr uploadDataProviderPtr);
        internal delegate void ReadFunc(IntPtr uploadDataProviderPtr, IntPtr uploadDataSinkPtr, IntPtr bufferPtr);
        internal delegate void RewindFunc(IntPtr uploadDataProviderPtr, IntPtr uploadDataSinkPtr);
        internal delegate void CloseFunc(IntPtr uploadDataProviderPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UploadDataProvider_Destroy(IntPtr uploadDataProviderPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern long Cronet_UploadDataProvider_GetLength(IntPtr uploadDataProviderPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UploadDataProvider_Read(IntPtr uploadDataProviderPtr, IntPtr uploadDataSinkPtr, IntPtr bufferPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UploadDataProvider_Rewind(IntPtr uploadDataProviderPtr, IntPtr dataSinkPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_UploadDataProvider_Close(IntPtr uploadDataProviderPtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_UploadDataProvider_CreateWith(
            GetLengthFunc getLengthFunc,
            ReadFunc readFunc,
            RewindFunc rewindFunc,
            CloseFunc closeFunc
        );
    }
}