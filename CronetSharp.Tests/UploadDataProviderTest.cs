using System;
using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class UploadDataProviderTest : SetupCronet
    {
        [Test]
        public void TestCanCreateUploadDataProvider()
        {
            using var provider = new UploadDataProvider();
            Assert.NotNull(provider);
        }
        
        [Test]
        public void TestUploadDataProvider()
        {
            bool success = false;
            ulong length = 10;
            bool isFinalChunk = true;

            using var buffer = ByteBuffer.Allocate(length);
            using var sink = new UploadDataSink
            {
                OnReadSucceeded = (uploadDataSink, byteBuffer) => success = true,
                OnReadError = err => success = false,
                OnRewindError = err => { },
                OnRewindSucceeded = () => { }
            };
            using var provider = new UploadDataProvider
            {
                OnRead = (uploadDataSink, byteBuffer) => uploadDataSink.NotifyReadSucceeded(length, isFinalChunk),
                OnGetLength = () => (long) length,
                OnRewind = _ => { },
                OnClose = () => { }
            };
            GC.Collect();
            provider.Read(sink, buffer);
            Assert.AreEqual(success, true);
        }
    }
}