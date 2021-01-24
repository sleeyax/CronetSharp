using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class HttpHeaderTest : SetupCronet
    {
        [Test]
        public void TestHttpHeader()
        {
            using var header = new HttpHeader("foo", "bar");
            Assert.AreEqual("foo", header.Name);
            Assert.AreEqual("bar", header.Value);
            header.Value = "baz";
            Assert.AreEqual("foo", header.Name);
            Assert.AreEqual("baz", header.Value);
            header.Dispose();
        }
    }
}