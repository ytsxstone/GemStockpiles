using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Abp.Application.Services;
using JFJT.GemStockpiles.Users.Dto;
using JFJT.GemStockpiles.Commons.Dto;

namespace JFJT.GemStockpiles.Commons
{
    public interface ICommonAppService : IApplicationService
    {
        /// <summary>
        /// 系统用户语言切换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ChangeLanguage(ChangeUserLanguageDto input);

        /// <summary>
        /// 系统用户密码修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ChangePassword(ChangePasswordDto input);

        /// <summary>
        /// 系统用户个人信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserDto> UpdateUserInfo(ChangeUserInfoDto input);

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="uploadType"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        Task<UploadFileResultDto> UploadFile(IFormFile file, int uploadType, int fileType);
    }
}
