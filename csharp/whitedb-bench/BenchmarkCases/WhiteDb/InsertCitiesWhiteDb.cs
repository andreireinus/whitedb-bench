namespace Benchmark.Cases.WhiteDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Benchmark.Cases.Models;

    using global::WhiteDb.Data;
    using global::WhiteDb.Data.Utils;

    public abstract class CitiesWhiteDbBase : BenchmarkCase
    {
        private const string DatabaseName = "9876";

        protected readonly Lazy<IList<City>> Cities = new Lazy<IList<City>>(
            () =>
            {
                var reader = new CititesReader();
                return reader.GetCities().ToList();
            });

        protected void DeleteDatabase()
        {
            try
            {
                DatabaseUtilites.DeleteDatabase(DatabaseName);
            }
            catch
            {
                // ignored
            }
        }

        protected DataContext BuildDataContext()
        {
            var dataContext = new DataContext(DatabaseName, DataSize.Size(1024).MegaBytes.ToInteger());
            foreach (var city in this.Cities.Value)
            {
                var record = dataContext.CreateRecord(6);

                record.SetFieldValue(0, city.Country);
                record.SetFieldValue(1, city.Name);
                record.SetFieldValue(2, city.Region);
                record.SetFieldValue(3, city.Population);
                record.SetFieldValue(4, city.Latitude);
                record.SetFieldValue(5, city.Longitude);
            }

            return dataContext;
        }
    }

    public class InsertCitiesWhiteDb : CitiesWhiteDbBase
    {
        public override void Setup()
        {
            var x = this.Cities.Value;
        }

        public override void BeforeRun()
        {
            this.DeleteDatabase();
        }

        public override void AfterRun()
        {
            this.DeleteDatabase();
        }

        public override bool Run()
        {
            using (this.BuildDataContext())
            {
                return true;
            }
        }
    }
}