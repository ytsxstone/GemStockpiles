using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using JFJT.GemStockpiles.Users.Dto;

namespace JFJT.GemStockpiles.Common
{
    public interface ICommonAppService: IApplicationService
    {
        //后台用户修改密码
        Task ChangePassword(ChangePasswordDto input);

        //后台用户修改个人信息
        Task<UserDto> UpdateUserInfo(ChangeUserInfoDto input);
    }
}
