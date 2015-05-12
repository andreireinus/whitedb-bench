namespace Benchmark.Cases.WhiteDb
{
    using System;

    using global::WhiteDb.Data;

    public class ApiQueryCaseIndex : ApiQueryCaseWgQuery
    {
        public override void Setup()
        {
            base.Setup();

            IndexApi.wg_create_index(this.DataContext.Pointer, 0, IndexApi.WG_INDEX_TYPE_TTREE, IntPtr.Zero, 0);
        }
    }
}