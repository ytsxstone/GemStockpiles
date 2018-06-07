using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Abp.Application.Services;
using JFJT.GemStockpiles.Helpers;
using JFJT.GemStockpiles.Users.Dto;
using JFJT.GemStockpiles.Commons.Dto;

namespace JFJT.GemStockpiles.Commons
{
    public interface ICommonAppService: IApplicationService
    {
        //后台用户修改密码
        Task ChangePassword(ChangePasswordDto input);

        //后台用户修改个人信息
        Task<UserDto> UpdateUserInfo(ChangeUserInfoDto input);

        //文件上传
        Task<UploadFileResultDto> UploadFile(IFormFile file, int uploadType, int fileType);
    }
}
