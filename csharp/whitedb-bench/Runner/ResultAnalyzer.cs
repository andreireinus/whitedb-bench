namespace Benchmark.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    internal class ResultAnalyzer
    {
        private readonly IReadOnlyCollection<long> times;
        private readonly CultureInfo cultureInfo = new CultureInfo("et-EE");

        public ResultAnalyzer(IList<long> times)
        {
            this.times = new ReadOnlyCollection<long>(times);
        }

        public void DumpConsole()
        {
            Console.WriteLine("Times: {0}", string.Join(", ", this.times.ToArray()));
            Console.WriteLine("Average: {0}", this.times.Average());
            Console.WriteLine("Max: {0}", this.times.Max());
            Console.WriteLine("Min: {0}", this.times.Min());
        }

        public void DumpFile(string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                foreach (var t in this.times)
                {
                    writer.WriteLine((t / 10000.0).ToString(this.cultureInfo));
                }
            }
        }
    }
}