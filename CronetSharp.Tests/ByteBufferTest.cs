using NUnit.Framework;

namespace CronetSharp.Tests
{
    public class ByteBufferTest : SetupCronet
    {
        private ulong _byteBufferSize;
        
        [SetUp]
        public void Setup()
        {
            base.Setup();
            _byteBufferSize = 20;
        }

        [Test]
        public void TestAllocation()
        {
            var buffer = new ByteBuffer(_byteBufferSize);
            var bufferData = buffer.GetData();
            Assert.AreEqual(_byteBufferSize, bufferData.Length);
            buffer.Destroy();
        }
    }
}