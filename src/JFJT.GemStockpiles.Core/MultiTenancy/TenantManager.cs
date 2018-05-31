using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using JFJT.GemStockpiles.Authorization.Users;
using JFJT.GemStockpiles.Editions;

namespace JFJT.GemStockpiles.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
