namespace Benchmark.Cases.WhiteDb
{
    using System;

    using global::WhiteDb.Data;

    public abstract class ApiInsertBase : BenchmarkCase
    {
        protected const string Name = "1";

        protected readonly int fieldCount;
        private readonly int recordCount;

        protected readonly int Size;

        protected ApiInsertBase(int size, int recordCount, int fieldCount)
        {
            this.Size = size;
            this.recordCount = recordCount;
            this.fieldCount = fieldCount;
        }

        public override bool Run()
        {
            var db = NativeApiWrapper.wg_attach_database(Name, this.Size);
            if (db == IntPtr.Zero)
            {
                return false;
            }

            if (!this.FillDatabase(db))
            {
                return false;
            }

            NativeApiWrapper.wg_detach_database(db);
            NativeApiWrapper.wg_delete_database(Name);

            return true;
        }

        protected bool FillDatabase(IntPtr db)
        {
            for (var i = 0; i < this.recordCount; i++)
            {
                var record = NativeApiWrapper.wg_create_record(db, this.fieldCount);
                if (record == IntPtr.Zero)
                {
                    Console.WriteLine("Failed at: {0}", i);
                    return false;
                }
                for (var j = 0; j < this.fieldCount; j++)
                {
                    NativeApiWrapper.wg_set_int_field(db, record, j, j);
                }
            }
            return true;
        }

        public override void Setup()
        {
            DeleteDatabase();

            base.Setup();
        }

        public override void TearDown()
        {
            DeleteDatabase();

            base.TearDown();
        }

        private static void DeleteDatabase()
        {
            var db = NativeApiWrapper.wg_attach_existing_database(Name);
            if (db != IntPtr.Zero)
            {
                NativeApiWrapper.wg_detach_database(db);
                NativeApiWrapper.wg_delete_database(Name);
            }
        }
    }
}