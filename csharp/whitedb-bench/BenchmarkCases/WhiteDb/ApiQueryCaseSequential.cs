namespace Benchmark.Cases.WhiteDb
{
    using System;
    using System.Linq;
    using System.Text;

    using global::WhiteDb.Data;

    public class ApiQueryCaseSequential : ApiQueryBase
    {
        public override bool Run()
        {
            var record = NativeApiWrapper.wg_get_first_record(this.Db);
            var count = 0;
            while (record != IntPtr.Zero)
            {
                var country = GetStringValue(this.Db, record, 0);
                if (country == "ee")
                {
                    count++;
                }
                record = NativeApiWrapper.wg_get_next_record(this.Db, record);
            }

            return (count > 0);
        }

        private static string GetStringValue(IntPtr db, IntPtr record, int index, bool debug = false)
        {
            var dataPointer = NativeApiWrapper.wg_get_field(db, record, index);

            var len = NativeApiWrapper.wg_decode_str_len(db, dataPointer) + 1;
            var bytes = new byte[len];

            NativeApiWrapper.wg_decode_str_copy(db, dataPointer, bytes, len);
            if (debug)
            {
                Console.WriteLine("{0}|{1}", bytes[0], bytes[1]);
            }
            return Encoding.Default.GetString(bytes.Take(len - 1).ToArray());
        }
    }
}