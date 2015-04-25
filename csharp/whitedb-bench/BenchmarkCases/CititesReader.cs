namespace Benchmark.Cases
{
    using System.Collections.Generic;
    using System.IO;

    using Benchmark.Cases.Models;

    public class CititesReader
    {
        public IEnumerable<City> GetCities()
        {
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

                    if (!isFirstLineRead)
                    {
                        isFirstLineRead = true;
                        continue;
                    }

                    var parts = line.Split(',');
                    yield return
                        new City
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

            double value;
            if (double.TryParse(s, out value))
            {
                return value;
            }
            return -1;
        }

        private static int ParsePopulation(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return -1;
            }

            int value;
            if (int.TryParse(s, out value))
            {
                return value;
            }
            return -1;
        }
    }
}