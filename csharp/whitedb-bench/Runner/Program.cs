namespace Benchmark.Runner
{
    using System;

    using Benchmark.Cases.STSdb;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var runner = new BenchmarkRunner<InsertCitiesStSdb>();
            var result = runner.Run();

            var analyzer = new ResultAnalyzer(result);
            analyzer.DumpResults();
            Console.ReadKey();
        }
    }
}