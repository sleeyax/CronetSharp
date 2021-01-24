using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class ExecutorTest : SetupCronet
    {
        [Test]
        public void TestCanExecuteRunnable()
        {
            bool ran = false;
            using var executor = new Executor();
            // NOTE: we don't use 'using' here, because runnable is already disposed by this executor!
            var runnable = new Runnable(() => ran = true);
            Assert.IsFalse(ran);
            executor.Execute(runnable);
            Assert.IsTrue(ran);
        }
    }
}