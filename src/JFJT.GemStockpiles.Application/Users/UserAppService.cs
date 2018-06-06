using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Extensions;
using Abp.Localization;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.IdentityFramework;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Roles.Dto;
using JFJT.GemStockpiles.Users.Dto;
using JFJT.GemStockpiles.Common.Dto;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Authorization.Roles;
using JFJT.GemStockpiles.Authorization.Users;
using Abp.UI;

namespace JFJT.GemStockpiles.Users
{
    [AbpAuthorize(PermissionNames.Pages_SystemManagement_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedResultRequestExtendDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
        }

        [AbpAuthorize(PermissionNames.Pages_SystemManagement_Users_Create)]
        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = _passwordHasher.HashPassword(user, input.Password);
            user.IsEmailConfirmed = true;

            CheckErrors(await _userManager.CreateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        [AbpAuthorize(PermissionNames.Pages_SystemManagement_Users_Edit)]
        public override async Task<UserDto> Update(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);
            var oldPassword = user.Password;

            MapToEntity(input, user);

            if (!string.IsNullOrWhiteSpace(input.Password) && !input.Password.Equals(oldPassword))
            {
                user.Password = _passwordHasher.HashPassword(user, input.Password);
            }

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }

        [AbpAuthorize(PermissionNames.Pages_SystemManagement_Users_Delete)]
        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task ChangePassword(ChangePasswordDto input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);

            CheckErrors(await _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword));
        }

        public async Task<UserDto> UpdateUserInfo(ChangeUserInfoDto input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);

            user.Name = input.Name;

            CheckErrors(await _userManager.UpdateAsync(user));

            return await Get(input);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            return userDto;
        }

        protected void MapToEntity(ChangeUserInfoDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestExtendDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles).WhereIf(!input.KeyWord.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.KeyWord) || x.Name.Contains(input.KeyWord));
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestExtendDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
