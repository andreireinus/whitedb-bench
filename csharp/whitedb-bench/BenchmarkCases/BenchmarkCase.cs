namespace Benchmark.Cases
{
    public abstract class BenchmarkCase
    {
        public abstract bool Run();

        public virtual void Setup()
        {
        }

        public virtual void TearDown()
        {
        }

        public virtual void BeforeRun()
        {
        }

        public virtual void AfterRun()
        {
        }
    }
}