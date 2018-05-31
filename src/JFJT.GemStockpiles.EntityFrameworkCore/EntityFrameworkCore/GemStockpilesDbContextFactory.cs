using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using JFJT.GemStockpiles.Configuration;
using JFJT.GemStockpiles.Web;

namespace JFJT.GemStockpiles.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class GemStockpilesDbContextFactory : IDesignTimeDbContextFactory<GemStockpilesDbContext>
    {
        public GemStockpilesDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GemStockpilesDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            GemStockpilesDbContextConfigurer.Configure(builder, configuration.GetConnectionString(GemStockpilesConsts.ConnectionStringName));

            return new GemStockpilesDbContext(builder.Options);
        }
    }
}
