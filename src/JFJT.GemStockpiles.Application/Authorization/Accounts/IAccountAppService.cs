using System.Threading.Tasks;
using Abp.Application.Services;
using JFJT.GemStockpiles.Authorization.Accounts.Dto;

namespace JFJT.GemStockpiles.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
