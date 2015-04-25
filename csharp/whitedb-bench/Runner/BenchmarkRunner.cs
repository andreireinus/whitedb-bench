namespace Benchmark.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Benchmark.Cases;

    internal class BenchmarkRunner<T> where T : BenchmarkCase, new()
    {
#if DEBUG
        private const int Count = 10;
#else
        private const int Count = 10;
#endif

        private readonly T benchmark = new T();

        internal List<long> Run()
        {
            this.benchmark.Setup();

            var times = new List<long>();
            for (var i = 0; i < Count; i++)
            {
                var sw = Stopwatch.StartNew();
                var result = this.benchmark.Run();
                sw.Stop();
                times.Add(sw.ElapsedMilliseconds);

                if (result)
                {
                    continue;
                }

                Console.WriteLine("Failed run at: {0}", i);
                break;
            }

            times.Average();

            this.benchmark.TearDown();

            return times;
        }
    }
}