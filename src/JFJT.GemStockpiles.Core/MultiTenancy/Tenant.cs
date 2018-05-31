using Abp.MultiTenancy;
using JFJT.GemStockpiles.Authorization.Users;

namespace JFJT.GemStockpiles.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
