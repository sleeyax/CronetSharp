using System;
using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class UploadDataSinkTest : SetupCronet
    {
        [Test]
        public void TestCanCreateUploadDataSink()
        {
            using var sink = new UploadDataSink();
            Assert.IsNotNull(sink);
        }
        
        [Test]
        public void TestCanUploadDataSinkCanRead()
        {
            bool success = false;
            using var sink = new UploadDataSink
            {
                OnReadSucceeded = (uploadDataSink, byteBuffer) => success = true,
                OnReadError = err => success = false,
                OnRewindError = err => { },
                OnRewindSucceeded = () => { }
            };
            GC.Collect();
            sink.NotifyReadSucceeded(10, true);
            Assert.AreEqual(success, true);
        }
    }
}