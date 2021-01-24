using System;
using System.IO;
using CronetSharp.Cronet.Asm;
using NUnit.Framework;

namespace CronetSharp.Tests
{
    public class FullyCustomizedDllLoader : ILoader
    {
        public bool loaded;
        
        public void Load(string path)
        {
            loaded = true;
        }
    }

    public class CustomLoader : CronetLoader
    {
        public bool loaded;
        
        public override void Load(string path)
        {
            loaded = true;
        }
    }
    
    public class DllLoaderTest
    {
        [Test]
        public void TestCustomILoaderImplementation()
        {
            var loader = new FullyCustomizedDllLoader();
            loader.Load("");
            Assert.AreEqual(true, loader.loaded);
        }
        
        [Test]
        public void TestCustomCronetLoaderImplementation()
        {
            var loader = new CustomLoader();
            loader.Load("");
            Assert.AreEqual(true, loader.loaded);
        }

        [Test]
        public void TestThrowErrorWhenDllNotFound()
        {
            var loader = new CronetLoader();
            Assert.Throws<ArgumentException>(() => loader.Load(""));
        }
    }
}