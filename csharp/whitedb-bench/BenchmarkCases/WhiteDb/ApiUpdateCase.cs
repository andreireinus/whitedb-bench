namespace Benchmark.Cases.WhiteDb
{
    using System;

    using global::WhiteDb.Data;

    public class ApiUpdateCase : ApiInsertCaseGigabyte
    {
        private IntPtr db;

        public override void Setup()
        {
            base.Setup();

            this.db = NativeApiWrapper.wg_attach_database(Name, this.Size);
            if (this.db == IntPtr.Zero)
            {
                throw new Exception("Unable to create database");
            }

            if (!this.FillDatabase(this.db))
            {
                throw new Exception("Unable to fill database");
            }
        }

        public override bool Run()
        {
            var record = NativeApiWrapper.wg_get_first_record(this.db);
            while (record != IntPtr.Zero)
            {
                for (var j = 0; j < this.fieldCount; j++)
                {
                    NativeApiWrapper.wg_set_int_field(this.db, record, j, j + 1);
                }
                record = NativeApiWrapper.wg_get_next_record(this.db, record);
            }
            return true;
        }
    }
}