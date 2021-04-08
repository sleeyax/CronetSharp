using System.Threading.Tasks;

namespace CronetSharp
{
    public static class Executors
    {
        /// <summary>
        /// Creates an Executor that uses a single worker thread.
        /// </summary>
        /// <param name="taskCreationOptions"></param>
        /// <returns></returns>
        public static Executor NewSingleThreadExecutor(TaskCreationOptions taskCreationOptions = TaskCreationOptions.LongRunning)
        {
            return new Executor(runnable =>
            {
                Task.Factory.StartNew(() =>
                {
                    runnable.Run();
                    runnable.Dispose();
                }, taskCreationOptions);
            });
        }
    }
}