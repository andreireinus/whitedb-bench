namespace Benchmark.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    internal class ResultAnalyzer
    {
        private readonly IReadOnlyCollection<long> times;

        public ResultAnalyzer(IList<long> times)
        {
            this.times = new ReadOnlyCollection<long>(times);
        }

        public void DumpResults()
        {
            Console.WriteLine("Times: {0}", string.Join(", ", this.times.ToArray()));
            Console.WriteLine("Average: {0}", this.times.Average());
            Console.WriteLine("Max: {0}", this.times.Max());
            Console.WriteLine("Min: {0}", this.times.Min());
        }
    }
}