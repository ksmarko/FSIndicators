using AspNetCoreAppPostgreSQL.Models;
using CsvHelper.Configuration;

namespace AspNetCoreAppPostgreSQL.Infrastructure
{
    public sealed class ARRClassMap : ClassMap<ARR>
    {
        public ARRClassMap()
        {
            //AutoMap();
            Map(m => m.Date).ConvertUsing(e => e.Date.ToString());
            Map(m => m.Profit).ConvertUsing(e => e.Profit.ToString());
        }
    }
}
