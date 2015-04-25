namespace Benchmark.Cases.WhiteDb
{
    using System;

    using global::WhiteDb.Data;
    using global::WhiteDb.Data.Utils;

    public class CitiesQueryWhiteDb : BenchmarkCase
    {
        private const string DatabaseName = "9876";

        private DataContext dataContext;

        public override void Setup()
        {
            DatabaseUtilites.DeleteDatabase(DatabaseName);

            this.dataContext = new DataContext(DatabaseName, DataSize.Size(1024).MegaBytes.ToInteger());
            var reader = new CititesReader();
            foreach (var city in reader.GetCities())
            {
                var record = this.dataContext.CreateRecord(6);

                record.SetFieldValue(0, city.Country);
                record.SetFieldValue(1, city.Name);
                record.SetFieldValue(2, city.Region);
                record.SetFieldValue(3, city.Population);
                record.SetFieldValue(4, city.Latitude);
                record.SetFieldValue(5, city.Longitude);
            }

            IndexApi.wg_create_index(this.dataContext.Pointer, 0, IndexApi.WG_INDEX_TYPE_TTREE, IntPtr.Zero, 0);
            IndexApi.wg_create_index(this.dataContext.Pointer, 1, IndexApi.WG_INDEX_TYPE_TTREE, IntPtr.Zero, 0);
            IndexApi.wg_create_index(this.dataContext.Pointer, 3, IndexApi.WG_INDEX_TYPE_TTREE, IntPtr.Zero, 0);

            base.Setup();
        }

        public override bool Run()
        {
            // 1 Leia kõik Eesti linnad
            if (!this.RunEstonianCityFinder())
            {
                return false;
            }

            // 2 Leia kõik linnad, mis on umbes samasuured
            if (!this.FindSimilarCitiesLikeTallinn())
            {
                return false;
            }

            return true;
        }

        private bool FindSimilarCitiesLikeTallinn()
        {
            int population = -2;

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

            const double Margin = 0.01;
            query = this.dataContext.CreateQueryBuilder()
                    .AddCondition(3, ConditionOperator.GreaterThanEqual, Convert.ToInt32(population * (1 - Margin)))
                    .AddCondition(3, ConditionOperator.GreaterThanEqual, Convert.ToInt32(population * (1 + Margin)))
                    .Execute();

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

        public override void Dispose()
        {
            if (this.dataContext == null)
            {
                return;
            }

            this.dataContext.Dispose();
            this.dataContext = null;
        }
    }
}