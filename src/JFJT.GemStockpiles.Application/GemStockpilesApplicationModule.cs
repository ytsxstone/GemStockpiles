using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JFJT.GemStockpiles.Authorization;

namespace JFJT.GemStockpiles
{
    [DependsOn(
        typeof(GemStockpilesCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class GemStockpilesApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<GemStockpilesAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(GemStockpilesApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
