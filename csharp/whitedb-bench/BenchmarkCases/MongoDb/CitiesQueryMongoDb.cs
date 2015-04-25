namespace Benchmark.Cases.MongoDb
{
    using System.Linq;

    using Benchmark.Cases.Models;

    using MongoDB.Driver;

    public class CitiesQueryMongoDb : BenchmarkCase
    {
        private const string DatabaseName = "cities";

        private MongoClient client;

        private IMongoDatabase database;

        public override void Setup()
        {
            this.client = new MongoClient("mongodb://localhost:27017");
            this.database = this.client.GetDatabase(DatabaseName);

            var collection = this.database.GetCollection<City>(DatabaseName);
            if (collection.CountAsync(a => a.Name != null).Result == 0)
            {
                var reader = new CititesReader();
                collection.InsertManyAsync(reader.GetCities()).Wait();

                collection.Indexes.CreateOneAsync(Builders<City>.IndexKeys.Ascending(a => a.Name));
                collection.Indexes.CreateOneAsync(Builders<City>.IndexKeys.Ascending(a => a.Country));
                collection.Indexes.CreateOneAsync(Builders<City>.IndexKeys.Ascending(a => a.Population));
            }

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
            var tallinn = this.database.GetCollection<City>(DatabaseName).Find(a => a.Name == "Tallinn").FirstAsync().Result;
            var population = tallinn.Population;

            const double Margin = 0.01;
            var count = this.database.GetCollection<City>(DatabaseName).Find(a => a.Population >= population * (1 - Margin) && a.Population <= population * (1 + Margin)).CountAsync().Result;

            return count != 0;
        }

        private bool RunEstonianCityFinder()
        {
            var cities = this.database.GetCollection<City>(DatabaseName).Find(a => a.Country == "ee").ToListAsync().Result;
            var count = cities.Count();

            return count != 0;
        }

        public override void Dispose()
        {
        }
    }
}