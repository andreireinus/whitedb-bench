namespace Benchmark.Cases.MongoDb
{
    using Benchmark.Cases.Models;

    using MongoDB.Driver;

    public class SelectCitiesMongoDb : InsertCitiesMongoDb
    {
        private IMongoCollection<MongoCity> cityCollection;

        public override void BeforeRun()
        {
        }

        public override void AfterRun()
        {
        }

        public override void Setup()
        {
            this.cityCollection = this.GetCollection();

            //this.cityCollection.Indexes.CreateOneAsync(Builders<MongoCity>.IndexKeys.Ascending(a => a.Name));
            //this.cityCollection.Indexes.CreateOneAsync(Builders<MongoCity>.IndexKeys.Ascending(a => a.Country));
            //this.cityCollection.Indexes.CreateOneAsync(Builders<MongoCity>.IndexKeys.Ascending(a => a.Population));

            this.Run();
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
            var tallinn = this.cityCollection.Find(a => a.Name == "Tallinn").FirstAsync().Result;
            var population = tallinn.Population;

            const double Margin = 0.01;
            var minPopulation = population * (1 - Margin);
            var maxPopulation = population * (1 + Margin);

            return this.cityCollection.CountAsync(a => a.Population >= minPopulation && a.Population <= maxPopulation).Result != 0;
        }

        private bool RunEstonianCityFinder()
        {
            return this.GetCollection().CountAsync(a => a.Country == "ee").Result != 0;
        }
    }
}