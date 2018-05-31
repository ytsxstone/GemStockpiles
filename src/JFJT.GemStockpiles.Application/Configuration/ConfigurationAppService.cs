using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using JFJT.GemStockpiles.Configuration.Dto;

namespace JFJT.GemStockpiles.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : GemStockpilesAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
