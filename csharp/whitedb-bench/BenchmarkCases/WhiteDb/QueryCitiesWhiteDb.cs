namespace Benchmark.Cases.WhiteDb
{
    using System;

    using global::WhiteDb.Data;

    public class QueryCitiesWhiteDb : CitiesWhiteDbBase
    {
        private const double Margin = 0.01;
        private DataContext dataContext;

        public override bool Run()
        {
            // 1 Leia kõik Eesti linnad
            if (!this.RunEstonianCityFinder())
            {
                return false;
            }

            // 2 Leia kõik linnad, mis on umbes samasuured
            return this.FindSimilarCitiesLikeTallinn();
        }

        public override void Setup()
        {
            this.DeleteDatabase();
            this.dataContext = this.BuildDataContext();

            IndexApi.wg_create_index(this.dataContext.Pointer, 0, IndexApi.WG_INDEX_TYPE_TTREE, IntPtr.Zero, 0);
            IndexApi.wg_create_index(this.dataContext.Pointer, 1, IndexApi.WG_INDEX_TYPE_TTREE, IntPtr.Zero, 0);
            IndexApi.wg_create_index(this.dataContext.Pointer, 3, IndexApi.WG_INDEX_TYPE_TTREE, IntPtr.Zero, 0);

            this.Run();
        }

        private bool FindSimilarCitiesLikeTallinn()
        {
            int population, count = 0;

            var query = this.dataContext.CreateQueryBuilder().AddCondition(1, ConditionOperator.Equal, "Tallinn").Execute();
            using (query)
            {
                var record = query.Fetch();
                if (record == null)
                {
                    return false;
                }

                population = record.GetFieldValueInteger(3);
            }

            var minPopulation = Convert.ToInt32(population * (1 - Margin));
            var maxPopulation = Convert.ToInt32(population * (1 + Margin));

            query = this.dataContext.CreateQueryBuilder()
                    .AddCondition(3, ConditionOperator.GreaterThanEqual, minPopulation)
                    .AddCondition(3, ConditionOperator.LessThanEqual, maxPopulation)
                    .Execute();

            using (query)
            {
                while (query.Fetch() != null)
                {
                    count++;
                }
            }

            return count != 0;
        }

        private bool RunEstonianCityFinder()
        {
            var query = this.dataContext.CreateQueryBuilder().AddCondition(0, ConditionOperator.Equal, "ee").Execute();
            using (query)
            {
                var record = query.Fetch();
                while (record != null)
                {
                    record = query.Fetch();
                }
            }

            return true;
        }
    }
}