namespace Benchmark.Runner
{
    using System;

    using Benchmark.Cases;
    using Benchmark.Cases.MongoDb;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var runner = new BenchmarkRunner<CitiesQueryMongoDb>();
            var result = runner.Run();

            var analyzer = new ResultAnalyzer(result);
            analyzer.DumpResults();
            Console.ReadKey();
        }
    }
}