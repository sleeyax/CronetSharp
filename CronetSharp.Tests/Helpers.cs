using System;

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
    }
}