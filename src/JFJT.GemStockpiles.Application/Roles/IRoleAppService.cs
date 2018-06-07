using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Roles.Dto;
using JFJT.GemStockpiles.Commons.Dto;

namespace JFJT.GemStockpiles.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestExtendDto, CreateRoleDto, RoleDto>
    {
        Task<RoleDto> GetRoleForEdit(int id);

        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<ListResultDto<PermissionTreeDto>> GetTreePermissions();
    }
}
