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
            bool readSuccess = false;
            bool rewindSuccess = false;
            ulong length = 10;
            bool isFinalChunk = false;

            using var buffer = ByteBuffer.Allocate(length);
            using var sink = new UploadDataSink
            {
                OnReadSucceeded = (uploadDataSink, byteBuffer) => readSuccess = true,
                OnReadError = err => readSuccess = false,
                OnRewindError = err => rewindSuccess = false,
                OnRewindSucceeded = () => rewindSuccess = true
            };
            using var provider = new UploadDataProvider
            {
                OnRead = (uploadDataSink, byteBuffer) => uploadDataSink.NotifyReadSucceeded(length, isFinalChunk),
                OnGetLength = () => (long) length,
                OnRewind = sink => sink.NotifyRewindSucceeded(),
                OnClose = () => { }
            };
            GC.Collect();
            provider.Read(sink, buffer);
            Assert.AreEqual( true, readSuccess);
            provider.Rewind(sink);
            Assert.AreEqual(true, rewindSuccess);
        }
    }
}