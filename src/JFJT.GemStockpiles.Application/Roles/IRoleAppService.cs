using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Roles.Dto;
using JFJT.GemStockpiles.Commons.Dto;

namespace JFJT.GemStockpiles.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestExtendDto, CreateRoleDto, RoleDto>
    {
        /// <summary>
        /// 根据ID获取编辑角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RoleDto> GetRoleForEdit(int id);

        /// <summary>
        /// 获取权限列表数据
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        /// <summary>
        /// 获取权限树形结构数据
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<PermissionTreeDto>> GetTreePermissions();
    }
}
