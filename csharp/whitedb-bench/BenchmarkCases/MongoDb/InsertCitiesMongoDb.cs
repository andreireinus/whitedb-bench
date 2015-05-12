namespace Benchmark.Cases.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Benchmark.Cases.Models;

    using MongoDB.Driver;

    public class InsertCitiesMongoDb : BenchmarkCase
    {
        private const string DatabaseName = "cities";

        private readonly IMongoDatabase database;

        public InsertCitiesMongoDb()
        {
            var settings = new MongoClientSettings { ConnectTimeout = TimeSpan.FromSeconds(1), Server = new MongoServerAddress("localhost", 27017) };
            var client = new MongoClient(settings);
            this.database = client.GetDatabase(DatabaseName);
        }

        public override void Setup()
        {
            var value = this.cities.Value;

            base.Setup();
        }

        public override bool Run()
        {
            var collection = this.GetCollection();

            return true;
        }

        private readonly Lazy<IList<MongoCity>> cities = new Lazy<IList<MongoCity>>(
            () =>
            {
                var reader = new CititesReader();
                return reader.GetCities<MongoCity>().ToList();
            });

        public override void BeforeRun()
        {
            this.database.DropCollectionAsync(DatabaseName).Wait();
        }

        protected IMongoCollection<MongoCity> GetCollection()
        {
            var collection = this.database.GetCollection<MongoCity>(DatabaseName);

            if (collection.CountAsync(a => a.Name != null).Result != 0)
            {
                return collection;
            }

            collection.InsertManyAsync(this.cities.Value).Wait();

            return collection;
        }
    }
}