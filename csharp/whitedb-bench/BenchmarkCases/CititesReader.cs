namespace Benchmark.Cases
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    using Benchmark.Cases.Models;

    public class CititesReader
    {
        public IEnumerable<City> GetCities()
        {
            return this.GetCities<City>();
        }

        public IEnumerable<T> GetCities<T>() where T : City, new()
        {
            int count = 0;
            using (var reader = new StreamReader("worldcitiespop.txt"))
            {
                var isFirstLineRead = false;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    if (count++ == 10000)
                    {
                        //yield break;
                    }
                    if (!isFirstLineRead)
                    {
                        isFirstLineRead = true;
                        continue;
                    }

                    var parts = line.Split(',');
                    yield return
                        new T
                        {
                            Country = parts[0],
                            Name = parts[2],
                            Region = parts[3],
                            Population = ParsePopulation(parts[4]),
                            Latitude = ParseCordinates(parts[5]),
                            Longitude = ParseCordinates(parts[6])
                        };
                }
            }
        }

        private static double ParseCordinates(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return -1;
            }

            return double.Parse(s, CultureInfo.InvariantCulture);
        }

        private static int ParsePopulation(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return -1;
            }

            return int.Parse(s);
        }
    }
}