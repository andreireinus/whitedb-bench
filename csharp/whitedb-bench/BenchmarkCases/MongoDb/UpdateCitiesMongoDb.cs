namespace Benchmark.Cases.MongoDb
{
    using Benchmark.Cases.Models;

    using MongoDB.Driver;

    public class UpdateCitiesMongoDb : InsertCitiesMongoDb
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

            this.cityCollection.Indexes.CreateOneAsync(Builders<MongoCity>.IndexKeys.Ascending(a => a.Name));
            this.cityCollection.Indexes.CreateOneAsync(Builders<MongoCity>.IndexKeys.Ascending(a => a.Country));
            this.cityCollection.Indexes.CreateOneAsync(Builders<MongoCity>.IndexKeys.Ascending(a => a.Population));

            this.Run();
        }

        public override bool Run()
        {
            this.cityCollection.UpdateManyAsync(
                Builders<MongoCity>.Filter.Where(a => a.Country == "ee"),
                Builders<MongoCity>.Update.Set(b => b.Population, 100)).Wait();

            return true;
        }
    }
}