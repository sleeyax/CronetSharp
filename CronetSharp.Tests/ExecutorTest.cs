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

        [Test]
        public void TestCanExecuteMultipleRunnable()
        {
            int count = 0;
            int max = 20;

            while (count < max)
            {
                using var executor = Executors.NewSingleThreadExecutor();
                // NOTE: DO NOT DISPOSE THE RUNNABLE BELOW MANUALLY! It is already disposed by the executor above.
                var runnable = new Runnable(() =>
                {
                    count += 1;
                });
                executor.Execute(runnable);
            }
            
            Assert.AreEqual(count, max);
        }
    }
}