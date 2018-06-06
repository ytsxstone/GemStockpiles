using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp;
using Abp.Authorization;
using Abp.IdentityFramework;
using JFJT.GemStockpiles.Users.Dto;
using JFJT.GemStockpiles.Authorization.Users;

namespace JFJT.GemStockpiles.Common
{
    /// <summary>
    /// 存放公共的API接口, 必须登录才能进行操作
    /// </summary>
    [AbpAuthorize]
    public class CommonAppService : AbpServiceBase, ICommonAppService
    {
        private readonly UserManager _userManager;

        public CommonAppService(UserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 后台用户修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChangePassword(ChangePasswordDto input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);

            CheckErrors(await _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword));
        }

        /// <summary>
        /// //后台用户修改个人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserDto> UpdateUserInfo(ChangeUserInfoDto input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);

            user.Name = input.Name;

            CheckErrors(await _userManager.UpdateAsync(user));

            return ObjectMapper.Map<UserDto>(user);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
