using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace JFJT.GemStockpiles.EntityFrameworkCore
{
    public static class GemStockpilesDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<GemStockpilesDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<GemStockpilesDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
