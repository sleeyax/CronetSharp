using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class DateTimeTest : SetupCronet
    {
        [Test]
        public void TestDateTime()
        {
            long time = 1611526959768;
            using var dt = new Datetime(time);
            Assert.AreEqual(time, dt.Value);
        }
    }
}