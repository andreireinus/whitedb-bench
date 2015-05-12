namespace Benchmark.Cases.WhiteDb
{
    using System;

    using global::WhiteDb.Data;

    public class ApiQueryCaseWgQuery : BenchmarkCase
    {
        protected DataContext DataContext;

        private const string DatabaseName = "1";

        public override void Setup()
        {
            this.DataContext = new DataContext(DatabaseName, DataSize.Size(900).MegaBytes.ToInteger());

            var reader = new CititesReader();
            foreach (var city in reader.GetCities())
            {
                var record = this.DataContext.CreateRecord(6);

                record.SetFieldValue(0, city.Country);
                record.SetFieldValue(1, city.Name);
                record.SetFieldValue(2, city.Region);
                record.SetFieldValue(3, city.Population);
                record.SetFieldValue(4, city.Latitude);
                record.SetFieldValue(5, city.Longitude);
            }

            base.Setup();
        }

        public override bool Run()
        {
            var count = 0;

            var query = this.DataContext.CreateQueryBuilder().AddCondition(0, ConditionOperator.Equal, "ee").Execute();
            using (query)
            {
                while (query.Fetch() != null)
                {
                    count++;
                }
            }
            return (count > 0);
        }

        public override void TearDown()
        {
            base.TearDown();

            this.DataContext.Dispose();
            NativeApiWrapper.wg_delete_database(DatabaseName);
        }
    }
}