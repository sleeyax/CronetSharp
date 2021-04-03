using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class CronetEngineParamsTest : SetupCronet
    {
        [Test]
        public void TestProxyIsNullByDefault()
        {
            using var engineParams = new CronetEngineParams();
            Assert.IsNull(engineParams.Proxy);
        }
    }
}
