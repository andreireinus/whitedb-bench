namespace Benchmark.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Benchmark.Cases;

    using WhiteDb.Data;

    internal class BenchmarkRunner
    {
        private readonly BenchmarkCase benchmark;

#if DEBUG
        private const int Count = 1;
#else
        private const int Count = 20;
#endif

        protected BenchmarkRunner(BenchmarkCase benchmark)
        {
            this.benchmark = benchmark;
        }

        internal List<long> Run()
        {
            Console.WriteLine("Enviroment: {0}", OsHelper.IsMono ? "Mono" : "Win32");
            Console.WriteLine("Setup started");
            this.benchmark.Setup();
            Console.WriteLine("Setup complete");

            var times = new List<long>();
            for (var i = 0; i < Count; i++)
            {
                this.benchmark.BeforeRun();
                var sw = Stopwatch.StartNew();
                Console.WriteLine("Run started, {0}", i);

                var result = this.benchmark.Run();

                Console.WriteLine("Run complete, {0}, {1}", i, result);

                sw.Stop();
                times.Add((long)(sw.Elapsed.TotalMilliseconds * 10));

                this.benchmark.AfterRun();

                if (result)
                {
                    continue;
                }

                Console.WriteLine("Failed run at: {0}", i);
                break;
            }

            //Console.WriteLine("TearDown started");
            this.benchmark.TearDown();
            //Console.WriteLine("TearDown complete");

            return times;
        }
    }

    internal class BenchmarkRunner<T> : BenchmarkRunner where T : BenchmarkCase, new()
    {
        internal BenchmarkRunner()
            : base(new T())
        {
        }
    }
}