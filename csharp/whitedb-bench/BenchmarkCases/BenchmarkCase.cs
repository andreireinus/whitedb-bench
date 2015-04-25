namespace Benchmark.Cases
{
    using System;

    public abstract class BenchmarkCase : IDisposable
    {
        public abstract bool Run();

        public virtual void Setup()
        {
        }

        public virtual void TearDown()
        {
        }

        public abstract void Dispose();
    }
}