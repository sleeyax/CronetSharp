using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class ProxyTest
    {
        [Test]
        public void TestNormalProxy()
        {
            var proxy = new Proxy("http://127.0.0.1:8888");
            Assert.AreEqual("127.0.0.1", proxy.Address);
            Assert.AreEqual(8888, proxy.Port);
        }
        
        [Test]
        public void TestNormalProxyWithAuth()
        {
            var proxy = new Proxy("http://127.0.0.1:8888:username:password");
            Assert.AreEqual("127.0.0.1", proxy.Address);
            Assert.AreEqual(8888, proxy.Port);
            Assert.AreEqual("username", proxy.Username);
            Assert.AreEqual("password", proxy.Password);
        }
        
        [Test]
        public void TestReverseNotationProxy()
        {
            var proxy = new Proxy("http://username:password@127.0.0.1:8888");
            Assert.AreEqual("127.0.0.1", proxy.Address);
            Assert.AreEqual(8888, proxy.Port);
            Assert.AreEqual("username", proxy.Username);
            Assert.AreEqual("password", proxy.Password);
        }
    }
}