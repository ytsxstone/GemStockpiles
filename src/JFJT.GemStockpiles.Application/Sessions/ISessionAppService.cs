using System.Threading.Tasks;
using Abp.Application.Services;
using JFJT.GemStockpiles.Sessions.Dto;

namespace JFJT.GemStockpiles.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
