using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace JFJT.GemStockpiles.Controllers
{
    public abstract class GemStockpilesControllerBase: AbpController
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
