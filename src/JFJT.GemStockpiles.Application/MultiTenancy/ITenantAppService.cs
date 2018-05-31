using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.MultiTenancy.Dto;

namespace JFJT.GemStockpiles.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
