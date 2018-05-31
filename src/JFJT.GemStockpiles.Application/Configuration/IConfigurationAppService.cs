using System.Threading.Tasks;
using JFJT.GemStockpiles.Configuration.Dto;

namespace JFJT.GemStockpiles.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
