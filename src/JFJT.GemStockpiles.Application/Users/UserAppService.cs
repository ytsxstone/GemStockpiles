using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.IdentityFramework;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Roles.Dto;
using JFJT.GemStockpiles.Users.Dto;
using JFJT.GemStockpiles.Commons.Dto;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Authorization.Roles;
using JFJT.GemStockpiles.Authorization.Users;

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

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_SystemManagement_Users_Delete)]
        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        /// <summary>
        /// 获取系统角色数据
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        /// <summary>
        /// Dto模型映射
        /// </summary>
        /// <param name="createInput"></param>
        /// <returns></returns>
        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        /// <summary>
        /// Dto模型映射
        /// </summary>
        /// <param name="input"></param>
        /// <param name="user"></param>
        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        /// <summary>
        /// Dto模型映射
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected override UserDto MapToEntityDto(User user)
        {
            var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            return userDto;
        }

        /// <summary>
        /// 根据ID获取Entity模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        /// <summary>
        /// GetAll查询过滤条件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestExtendDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles).WhereIf(!input.KeyWord.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.KeyWord) || x.Name.Contains(input.KeyWord));
        }

        /// <summary>
        /// GetAll查询排序条件
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestExtendDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        /// <summary>
        /// 异常描述本地化转换函数
        /// </summary>
        /// <param name="identityResult"></param>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
