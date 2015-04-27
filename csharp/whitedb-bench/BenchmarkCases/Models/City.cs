namespace Benchmark.Cases.Models
{
    using MongoDB.Bson;

    public class City
    {
        public string Country { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }

        public int Population { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public override int GetHashCode()
        {
            var v = 1203787;
            v = (v * 28341) + this.Country.GetHashCode();
            v = (v * 28341) + this.Name.GetHashCode();
            v = (v * 28341) + this.Region.GetHashCode();
            v = (v * 28341) + this.Population.GetHashCode();
            v = (v * 28341) + this.Latitude.GetHashCode();
            v = (v * 28341) + this.Longitude.GetHashCode();

            return v;
        }
    }
}