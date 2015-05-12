namespace Benchmark.Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Benchmark.Cases.MongoDb;
    using Benchmark.Cases.WhiteDb;

    public static class Program
    {
        private static readonly Dictionary<string, BenchmarkRunner> Config = new Dictionary<string, BenchmarkRunner>();

        public static void Main(string[] args)
        {
            BuildConfigurations();

            var run = false;
            List<long> result;
            ResultAnalyzer analyzer;

            foreach (var key in args)
            {
                if (Config.ContainsKey(key))
                {
                    Console.WriteLine("Running {0}", key);
                    var r = Config[key];
                    result = r.Run();
                    run = true;

                    analyzer = new ResultAnalyzer(result);
                    analyzer.DumpFile(Path.Combine(@"../../results/", string.Format("csharp-{0}.txt", key)));
                }

                if (run)
                {
                    return;
                }
            }

            var runner = new BenchmarkRunner<SelectCitiesMongoDb>();
            result = runner.Run();
            Console.WriteLine("-> Analyze");
            analyzer = new ResultAnalyzer(result);
            analyzer.DumpConsole();
            Console.WriteLine("<- Analyze");
            Console.ReadLine();
        }

        private static void BuildConfigurations()
        {
            Config.Add("mongodb-insert", new BenchmarkRunner<InsertCitiesMongoDb>());
            Config.Add("mongodb-select", new BenchmarkRunner<SelectCitiesMongoDb>());
            Config.Add("mongodb-update", new BenchmarkRunner<UpdateCitiesMongoDb>());

            Config.Add("whitedb-insert", new BenchmarkRunner<InsertCitiesWhiteDb>());
            Config.Add("whitedb-select", new BenchmarkRunner<QueryCitiesWhiteDb>());
            Config.Add("whitedb-update", new BenchmarkRunner<UpdateCitiesWhiteDb>());

            // Api tests
            Config.Add("api-insert-case1", new BenchmarkRunner<ApiInsertCaseSmall>());
            Config.Add("api-insert-case2", new BenchmarkRunner<ApiInsertCaseGigabyte>());
            Config.Add("api-update-case", new BenchmarkRunner<ApiUpdateCase>());
            Config.Add("api-query-case1", new BenchmarkRunner<ApiQueryCaseSequential>());
            Config.Add("api-query-case2", new BenchmarkRunner<ApiQueryCaseWgQuery>());
            Config.Add("api-query-case3", new BenchmarkRunner<ApiQueryCaseIndex>());
        }
    }
}