using Microsoft.AspNetCore.Identity;
using Abp.IdentityFramework;
using Abp.AspNetCore.Mvc.Controllers;

namespace JFJT.GemStockpiles.Controllers
{
    public abstract class GemStockpilesControllerBase : AbpController
    {
        protected GemStockpilesControllerBase()
        {
            LocalizationSourceName = GemStockpilesConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
