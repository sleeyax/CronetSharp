using System;
using CronetSharp.Cronet.Bin;
using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class FullyCustomizedDllLoader : ILoader
    {
        public bool loaded;
        
        public bool Load(string path)
        {
            loaded = true;
            return loaded;
        }
    }

    public class CustomLoader : CronetLoader
    {
        public bool loaded;
        
        public override bool Load(string path)
        {
            loaded = true;
            return loaded;
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