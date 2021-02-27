using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class ProxyImplTest : SetupCronet
    {
        [Test]
        public void TestProxyImplementation()
        {
            var proxy = new Proxy("http://abc:def@127.0.0.1:8888");
            using var engineParams = new CronetEngineParams
            {
                Proxy = proxy
            };
            Assert.AreEqual(proxy.ToString(), engineParams.Proxy.ToString());
        }
    }
}