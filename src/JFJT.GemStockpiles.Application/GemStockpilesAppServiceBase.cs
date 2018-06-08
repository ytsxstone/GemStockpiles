using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Runtime.Session;
using Abp.IdentityFramework;
using Abp.Application.Services;
using JFJT.GemStockpiles.MultiTenancy;
using JFJT.GemStockpiles.Authorization.Users;

namespace JFJT.GemStockpiles
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class GemStockpilesAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected GemStockpilesAppServiceBase()
        {
            LocalizationSourceName = GemStockpilesConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
