using System;
using System.Runtime.InteropServices;
using CronetSharp.Cronet.Marshalers;

namespace CronetSharp.Cronet
{
    public abstract class UrlRequestCallback
    {
        internal abstract void OnRedirectReceived(
            IntPtr urlRequestPtr,
            IntPtr urlResponseInfoPtr,
            string newLocationUrl
        );
        internal abstract void OnResponseStarted(IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr);
        internal abstract void OnReadCompleted(
            IntPtr urlRequestPtr,
            IntPtr urlResponseInfoPtr,
            IntPtr bufferPtr,
            ulong bytesRead
        );
        internal abstract void OnSucceeded(IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr);
        internal abstract void OnFailed(
            IntPtr urlRequestPtr,
            IntPtr urlResponseInfoPtr,
            IntPtr errorPtr
        );
        internal abstract void OnCanceled(IntPtr urlRequestPtr, IntPtr urlResponseInfoPtr);
    }
}