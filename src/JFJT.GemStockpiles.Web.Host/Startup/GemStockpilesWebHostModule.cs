using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JFJT.GemStockpiles.Configuration;

namespace JFJT.GemStockpiles.Web.Host.Startup
{
    [DependsOn(
       typeof(GemStockpilesWebCoreModule))]
    public class GemStockpilesWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public GemStockpilesWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GemStockpilesWebHostModule).GetAssembly());
        }
    }
}
