using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JFJT.GemStockpiles.Configuration;
using JFJT.GemStockpiles.EntityFrameworkCore;
using JFJT.GemStockpiles.Migrator.DependencyInjection;

namespace JFJT.GemStockpiles.Migrator
{
    [DependsOn(typeof(GemStockpilesEntityFrameworkModule))]
    public class GemStockpilesMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public GemStockpilesMigratorModule(GemStockpilesEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(GemStockpilesMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                GemStockpilesConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GemStockpilesMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
