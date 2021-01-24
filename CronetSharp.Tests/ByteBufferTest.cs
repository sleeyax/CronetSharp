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
        public void TestBufferSizeAllocation()
        {
            var buffer = new ByteBuffer(_byteBufferSize);
            var bufferData = buffer.GetData();
            Assert.AreEqual(_byteBufferSize, bufferData.Length);
            Assert.AreEqual(_byteBufferSize, buffer.GetSize());
            buffer.Destroy();
        }

        [Test]
        public void TestBufferContentsTheSame()
        {
            byte[] data = Helpers.GenRandomBytes((int) _byteBufferSize);
            var buffer = new ByteBuffer(_byteBufferSize, data);
            Assert.AreEqual(data, buffer.GetData());
            buffer.Destroy();
        }

        [Test]
        public void TestBufferCallback()
        {
            byte[] data = Helpers.GenRandomBytes((int) _byteBufferSize);
            bool destroyed = false;
            var buffer = new ByteBuffer(_byteBufferSize, data, new ByteBufferCallback(_ => destroyed = true));
            buffer.Destroy();
            Assert.AreEqual(true, destroyed);
        }

        [Test]
        public void TestBufferIsClearable()
        {
            byte[] empty = new byte[_byteBufferSize];
            var buffer = new ByteBuffer(_byteBufferSize, Helpers.GenRandomBytes((int) _byteBufferSize));
            Assert.AreNotEqual(empty, buffer.GetData());
            buffer.Clear();
            Assert.AreEqual(empty, buffer.GetData());
            buffer.Destroy();
        }
    }
}