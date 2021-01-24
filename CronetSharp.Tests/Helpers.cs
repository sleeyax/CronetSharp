using System;
using System.IO;

namespace CronetSharp.Tests
{
    public class Helpers
    {
        public static byte[] GenRandomBytes(int size)
        {
            Byte[] bytes = new Byte[size];
            new Random().NextBytes(bytes);
            return bytes;
        }
        
        public static string CreateTemporaryDirectory()
        {
            string tmpDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tmpDir);
            return tmpDir;
        }
    }
}