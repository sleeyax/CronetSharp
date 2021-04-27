using System;
using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
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
            using var buffer = new ByteBuffer(_byteBufferSize);
            byte[] data = buffer.GetData();
            Assert.AreEqual(_byteBufferSize, data.Length);
            Assert.AreEqual(_byteBufferSize, buffer.GetSize());
        }

        [Test]
        public void TestBufferContentsTheSame()
        {
            byte[] data = Helpers.GenRandomBytes((int) _byteBufferSize);
            using var buffer = new ByteBuffer(_byteBufferSize, data);
            Assert.AreEqual(data, buffer.GetData());
        }

        [Test]
        public void TestBufferCallback()
        {
            byte[] data = Helpers.GenRandomBytes((int) _byteBufferSize);
            bool destroyed = false;
            var buffer = new ByteBuffer(_byteBufferSize, data, new ByteBufferCallback(_ => destroyed = true));
            GC.Collect();
            buffer.Dispose();
            Assert.AreEqual(true, destroyed);
        }

        [Test]
        public void TestBufferIsClearable()
        {
            byte[] empty = new byte[_byteBufferSize];
            using var buffer = new ByteBuffer(_byteBufferSize, Helpers.GenRandomBytes((int) _byteBufferSize));
            Assert.AreNotEqual(empty, buffer.GetData());
            buffer.Clear();
            Assert.AreEqual(empty, buffer.GetData());
        }
    }
}