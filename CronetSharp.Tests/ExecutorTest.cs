using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class ExecutorTest
    {
        public void TestCanExecuteRunnable()
        {
            bool ran = false;
            using var runnable = new Runnable(() => ran = true);
            using var executor = new Executor();
            Assert.IsFalse(ran);
            executor.Execute(runnable);
            Assert.IsTrue(ran);
        }
        
        
    }
}