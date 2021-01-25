using CronetSharp;

namespace example
{
    public interface IExample
    {
        void Run(CronetEngine cronetEngine);
        void Dispose();
    }
}