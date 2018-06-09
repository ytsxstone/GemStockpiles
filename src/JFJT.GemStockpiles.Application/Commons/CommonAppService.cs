using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Abp.UI;
using Abp.Authorization;
using JFJT.GemStockpiles.Helpers;
using JFJT.GemStockpiles.Users.Dto;
using JFJT.GemStockpiles.Commons.Dto;
using JFJT.GemStockpiles.Models.Configs;
using JFJT.GemStockpiles.Authorization.Users;

namespace JFJT.GemStockpiles.Commons
{
    /// <summary>
    /// 存放公共的API接口, 必须登录才能进行操作
    /// </summary>
    [AbpAuthorize]
    public class CommonAppService : GemStockpilesAppServiceBase, ICommonAppService
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly UploadHelper uploadHelper;

        public CommonAppService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
            uploadHelper = new UploadHelper(appSettings);
        }

        /// <summary>
        /// 后台用户修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChangePassword(ChangePasswordDto input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);

            CheckErrors(await UserManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword));
        }

        /// <summary>
        /// //后台用户修改个人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserDto> UpdateUserInfo(ChangeUserInfoDto input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);

            user.Name = input.Name;

            CheckErrors(await UserManager.UpdateAsync(user));

            return ObjectMapper.Map<UserDto>(user);
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public async Task<UploadFileResultDto> UploadFile(IFormFile file, int uploadType, int fileType)
        {
            //枚举值转换
            UploadType eUploadType = (UploadType)Enum.ToObject(typeof(UploadType), uploadType);
            FileType eFileType = (FileType)Enum.ToObject(typeof(FileType), fileType);

            var validate = uploadHelper.Validate(file, eUploadType, eFileType, out string error);
            if (!validate)
            {
                throw new UserFriendlyException("上传失败", error);
            }

            //获取文件保存信息
            var saveResult = await uploadHelper.SaveFile(file, AbpSession.UserId);

            return new UploadFileResultDto { FileName = saveResult.FileName, FilePath = saveResult.RelativePath };
        }
    }
}
