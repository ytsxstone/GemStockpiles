using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JFJT.GemStockpiles.Configuration;

namespace JFJT.GemStockpiles.Web.AppApi.Startup
{
    [DependsOn(
       typeof(GemStockpilesWebCoreModule))]
    public class GemStockpilesWebAppApiModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public GemStockpilesWebAppApiModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GemStockpilesWebAppApiModule).GetAssembly());
        }
    }
}
