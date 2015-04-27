namespace Benchmark.Cases.STSdb
{
    using System;
    using System.Linq;

    using Benchmark.Cases.Models;

    using STSdb4.Database;

    public class InsertCitiesStSdb : BenchmarkCase
    {
        private City[] cities;

        private const string TableName = "cities";

        public override void Setup()
        {
            var reader = new CititesReader();
            this.cities = reader.GetCities().ToArray();

            base.Setup();
        }

        public override bool Run()
        {
            using (var db = STSdb.FromMemory())
            {
                db.Delete(TableName);
                var table = db.OpenXTablePortable<Guid, City>(TableName);
                foreach (var city in this.cities)
                {
                    table.InsertOrIgnore(Guid.NewGuid(), city);
                }
            }

            return true;
        }

        public override void Dispose()
        {
        }
    }
}