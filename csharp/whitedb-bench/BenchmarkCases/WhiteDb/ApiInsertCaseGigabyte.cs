namespace Benchmark.Cases.WhiteDb
{
    using global::WhiteDb.Data;

    public class ApiInsertCaseGigabyte : ApiInsertBase
    {
        public ApiInsertCaseGigabyte()
            : base(DataSize.Size(1024).MegaBytes.ToInteger(), 680000, 100)
        {
        }

        public override bool Run()
        {
            var result = base.Run();

            return result;
        }
    }
}