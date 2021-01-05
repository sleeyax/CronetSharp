using System.Runtime.InteropServices;

namespace CronetSharp
{
    public class CronetEngine
    {
        [DllImport(@"D:\Documents\Programming\chromium\src\out\Debug\CronetSharp.89.0.4378.0.dll")]
        private static extern void Cronet_Engine_Create();
        
        
    }
}