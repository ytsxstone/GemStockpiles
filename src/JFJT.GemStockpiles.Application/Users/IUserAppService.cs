using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Roles.Dto;
using JFJT.GemStockpiles.Users.Dto;
using JFJT.GemStockpiles.Commons.Dto;

namespace JFJT.GemStockpiles.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestExtendDto, CreateUserDto, UserDto>
    {
        /// <summary>
        /// 获取系统角色数据
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}
