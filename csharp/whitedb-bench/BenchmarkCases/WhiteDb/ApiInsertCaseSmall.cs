namespace Benchmark.Cases.WhiteDb
{
    using global::WhiteDb.Data;

    public class ApiInsertCaseSmall : ApiInsertBase
    {
        public ApiInsertCaseSmall()
            : base(DataSize.Size(55).MegaBytes.ToInteger(), 68000, 100)
        {
        }
    }
}