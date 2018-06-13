using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Abp.UI;
using Abp.Localization;
using Abp.Authorization;
using Abp.Runtime.Session;
using JFJT.GemStockpiles.Helpers;
using JFJT.GemStockpiles.Users.Dto;
using JFJT.GemStockpiles.Commons.Dto;
using JFJT.GemStockpiles.Models.Configs;
using JFJT.GemStockpiles.Authorization.Users;

namespace JFJT.GemStockpiles.Commons
{
    /// <summary>
    /// 存放公共的API接口(登录后调用)
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
        /// 系统用户语言切换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        /// <summary>
        /// 系统用户密码修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChangePassword(ChangePasswordDto input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);

            CheckErrors(await UserManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword));
        }

        /// <summary>
        /// 系统用户个人信息修改
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
