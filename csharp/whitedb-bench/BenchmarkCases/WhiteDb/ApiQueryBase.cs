namespace Benchmark.Cases.WhiteDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Benchmark.Cases.Models;

    using global::WhiteDb.Data;
    using global::WhiteDb.Data.Utils;

    public class ApiQueryBase : BenchmarkCase
    {
        private const string DatabaseName = "33";

        private readonly Lazy<IList<City>> cities = new Lazy<IList<City>>(
            () =>
            {
                var reader = new CititesReader();
                return reader.GetCities().ToList();
            });

        protected IntPtr Db;

        public override bool Run()
        {
            return true;
        }

        public override void TearDown()
        {
            if (this.Db != IntPtr.Zero)
            {
                NativeApiWrapper.wg_detach_database(this.Db);
            }

            DatabaseUtilites.DeleteDatabase(DatabaseName);
        }

        public override void Setup()
        {
            base.Setup();

            //DatabaseUtilites.EmptyDatabase(DatabaseName);

            this.Db = NativeApiWrapper.wg_attach_database(DatabaseName, DataSize.Size(2048 - 100).MegaBytes.ToInteger());
            foreach (var city in this.cities.Value)
            {
                var record = NativeApiWrapper.wg_create_record(this.Db, 6);

                NativeApiWrapper.wg_set_str_field(this.Db, record, 0, city.Country);
                NativeApiWrapper.wg_set_str_field(this.Db, record, 1, city.Name);
                NativeApiWrapper.wg_set_str_field(this.Db, record, 2, city.Region);
                NativeApiWrapper.wg_set_int_field(this.Db, record, 3, city.Population);
                NativeApiWrapper.wg_set_field(this.Db, record, 4, NativeApiWrapper.wg_encode_double(this.Db, city.Longitude));
                NativeApiWrapper.wg_set_field(this.Db, record, 5, NativeApiWrapper.wg_encode_double(this.Db, city.Latitude));
            }
        }
    }
}