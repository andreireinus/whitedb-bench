namespace Benchmark.Cases.Models
{
    using MongoDB.Bson;

    public class City
    {
        public ObjectId Id { get; set; }

        public string Country { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }

        public int Population { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}