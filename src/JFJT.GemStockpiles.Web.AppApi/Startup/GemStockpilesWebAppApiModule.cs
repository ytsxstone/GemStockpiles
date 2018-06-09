using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using System.Globalization;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore;
using System.Reflection;
using Abp.Application.Services;

namespace JFJT.GemStockpiles.Web.Host.Startup
{
    [DependsOn(
        typeof(GemStockpilesApplicationModule),
        typeof(AbpAspNetCoreModule)
        )]
    public class GemStockpilesWebAppApiModule: AbpModule
    {
        private readonly IHostingEnvironment _env;

        public GemStockpilesWebAppApiModule(IHostingEnvironment env)
        {
            _env = env;
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(typeof(GemStockpilesApplicationModule).Assembly,
                moduleName: "app", useConventionalHttpVerbs: true);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
