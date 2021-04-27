using System;
using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class RunnableTest : SetupCronet
    {
        [Test]
        public void TestCanRunRunnable()
        {
            bool ran = false;
            var runnable = new Runnable(() => ran = true);
            GC.Collect();
            runnable.Run();
            runnable.Dispose();
            Assert.AreEqual(ran, true);
        }
    }
}