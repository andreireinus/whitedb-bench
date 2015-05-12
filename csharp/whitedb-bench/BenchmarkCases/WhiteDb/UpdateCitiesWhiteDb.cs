namespace Benchmark.Cases.WhiteDb
{
    using System;

    using global::WhiteDb.Data;

    public class UpdateCitiesWhiteDb : CitiesWhiteDbBase
    {
        private DataContext dataContext;

        public override void Setup()
        {
            this.DeleteDatabase();
            this.dataContext = this.BuildDataContext();

            IndexApi.wg_create_index(this.dataContext.Pointer, 0, IndexApi.WG_INDEX_TYPE_TTREE, IntPtr.Zero, 0);
            IndexApi.wg_create_index(this.dataContext.Pointer, 1, IndexApi.WG_INDEX_TYPE_TTREE, IntPtr.Zero, 0);
            IndexApi.wg_create_index(this.dataContext.Pointer, 3, IndexApi.WG_INDEX_TYPE_TTREE, IntPtr.Zero, 0);

            this.Run();
        }

        public override bool Run()
        {
            var result = false;
            using (var query = this.dataContext.CreateQueryBuilder().AddCondition(0, ConditionOperator.Equal, "ee").Execute())
            {
                DataRecord record;
                while ((record = query.Fetch()) != null)
                {
                    result = true;
                    record.SetFieldValue(3, 100);
                }
            }

            return result;
        }
    }
}