using System;
using System.Runtime.InteropServices;
using CronetSharp.CronetAsm;

namespace CronetSharp.Cronet
{
    internal static class Engine
    {
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Engine_Create();

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Engine_Destroy(IntPtr enginePtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern CronetEngineResult Cronet_Engine_Shutdown(IntPtr enginePtr);
        
        [DllImport(CronetLoader.Dll)]
        internal static extern CronetEngineResult Cronet_Engine_StartWithParams(IntPtr enginePtr, IntPtr engineParamsPtr);
            
        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Engine_AddRequestFinishedListener(IntPtr enginePtr, IntPtr requestFinishedListenerPtr, IntPtr executorPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Engine_RemoveRequestFinishedListener(IntPtr enginePtr, IntPtr requestFinishedInfoListenerPtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Engine_GetClientContext(IntPtr enginePtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern string Cronet_Engine_GetDefaultUserAgent(IntPtr enginePtr);
            
        [DllImport(CronetLoader.Dll)]
        internal static extern IntPtr Cronet_Engine_GetStreamEngine(IntPtr enginePtr);
            
        [DllImport(CronetLoader.Dll)]
        internal static extern string Cronet_Engine_GetVersionString(IntPtr enginePtr);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Engine_SetClientContext(IntPtr enginePtr, IntPtr clientContext);
            
        [DllImport(CronetLoader.Dll)]
        internal static extern bool Cronet_Engine_StartNetLogToFile(IntPtr enginePtr, string fileName, bool logAll);

        [DllImport(CronetLoader.Dll)]
        internal static extern void Cronet_Engine_StopNetLog(IntPtr enginePtr);
    }
}