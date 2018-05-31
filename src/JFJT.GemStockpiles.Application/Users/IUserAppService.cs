using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Roles.Dto;
using JFJT.GemStockpiles.Users.Dto;
using JFJT.GemStockpiles.Common.Dto;

namespace JFJT.GemStockpiles.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestExtendDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
