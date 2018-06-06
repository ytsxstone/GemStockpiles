using System;
using System.Threading.Tasks;
using Abp.Authorization;
using JFJT.GemStockpiles.Users;
using JFJT.GemStockpiles.Users.Dto;

namespace JFJT.GemStockpiles.Common
{
    /// <summary>
    /// 存放公共的API接口, 必须登录才能进行操作
    /// </summary>
    [AbpAuthorize]
    public class CommonAppService : ICommonAppService
    {
        private readonly UserAppService _userAppService;

        public CommonAppService(UserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// 后台用户修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChangePassword(ChangePasswordDto input)
        {
            await _userAppService.ChangePassword(input);
        }

        /// <summary>
        /// //后台用户修改个人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserDto> UpdateUserInfo(ChangeUserInfoDto input)
        {
            return await _userAppService.UpdateUserInfo(input);
        }
    }
}
