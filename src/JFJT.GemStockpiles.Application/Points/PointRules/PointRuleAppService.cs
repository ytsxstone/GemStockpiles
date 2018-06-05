using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Abp.Authorization;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Points;
using JFJT.GemStockpiles.Points.PointRules.Dto;

namespace JFJT.GemStockpiles.Points.PointRules
{
    [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRules)]
    public class PointRuleAppService : AsyncCrudAppService<PointRule, PointRuleDto, Guid, PagedResultRequestDto, PointRuleDto, PointRuleDto>, IPointRuleAppService
    {
        private readonly IRepository<PointRule, Guid> _pointRuleRepository;

        public PointRuleAppService(IRepository<PointRule, Guid> pointRuleRepository)
            : base(pointRuleRepository)
        {
            _pointRuleRepository = pointRuleRepository;
        }

        /// <summary>
        /// 获取所有积分动作列表
        /// </summary>
        /// <returns></returns>
        public Task<ListResultDto<PointActionDto>> GetAllPointActions()
        {
            List<PointActionDto> actions = new List<PointActionDto>();

            string[] keys = Enum.GetNames(typeof(PointActionEnum));
            Array values = Enum.GetValues(typeof(PointActionEnum));

            string actionName = "";
            for (int i = 0; i < keys.Length; i++)
            {
                switch (keys[i])
                {
                    case "Upload":
                        actionName = "上传商品";
                        break;
                    case "Buy":
                        actionName = "购买商品";
                        break;
                    case "Register":
                        actionName = "注册";
                        break;
                    case "Recommend":
                        actionName = "推荐";
                        break;
                    default:
                        actionName = "未定义";
                        break;
                }

                actions.Add(new PointActionDto() { Id = (int)values.GetValue(i), Name = actionName });
            }

            return Task.FromResult(new ListResultDto<PointActionDto>(
                ObjectMapper.Map<List<PointActionDto>>(actions)
            ));
        }

        //public override async Task<UserDto> Create(CreateUserDto input)
        //{
        //    CheckCreatePermission();

        //    var user = ObjectMapper.Map<User>(input);

        //    user.TenantId = AbpSession.TenantId;
        //    user.Password = _passwordHasher.HashPassword(user, input.Password);
        //    user.IsEmailConfirmed = true;

        //    CheckErrors(await _userManager.CreateAsync(user));

        //    if (input.RoleNames != null)
        //    {
        //        CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
        //    }

        //    CurrentUnitOfWork.SaveChanges();

        //    return MapToEntityDto(user);
        //}

        //public override async Task<UserDto> Update(UserDto input)
        //{
        //    CheckUpdatePermission();

        //    var user = await _userManager.GetUserByIdAsync(input.Id);

        //    MapToEntity(input, user);

        //    CheckErrors(await _userManager.UpdateAsync(user));

        //    if (input.RoleNames != null)
        //    {
        //        CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
        //    }

        //    return await Get(input);
        //}

        //public override async Task Delete(EntityDto<long> input)
        //{
        //    var user = await _userManager.GetUserByIdAsync(input.Id);
        //    await _userManager.DeleteAsync(user);
        //}

        //public async Task<ListResultDto<RoleDto>> GetRoles()
        //{
        //    var roles = await _roleRepository.GetAllListAsync();
        //    return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        //}

        //public async Task ChangeLanguage(ChangeUserLanguageDto input)
        //{
        //    await SettingManager.ChangeSettingForUserAsync(
        //        AbpSession.ToUserIdentifier(),
        //        LocalizationSettingNames.DefaultLanguage,
        //        input.LanguageName
        //    );
        //}

        //protected override User MapToEntity(CreateUserDto createInput)
        //{
        //    var user = ObjectMapper.Map<User>(createInput);
        //    user.SetNormalizedNames();
        //    return user;
        //}

        //protected override void MapToEntity(UserDto input, User user)
        //{
        //    ObjectMapper.Map(input, user);
        //    user.SetNormalizedNames();
        //}

        //protected override UserDto MapToEntityDto(User user)
        //{
        //    var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
        //    var userDto = base.MapToEntityDto(user);
        //    userDto.RoleNames = roles.ToArray();
        //    return userDto;
        //}

        //protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestDto input)
        //{
        //    return Repository.GetAllIncluding(x => x.Roles);
        //}

        //protected override async Task<User> GetEntityByIdAsync(long id)
        //{
        //    var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

        //    if (user == null)
        //    {
        //        throw new EntityNotFoundException(typeof(User), id);
        //    }

        //    return user;
        //}

        //protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestDto input)
        //{
        //    return query.OrderBy(r => r.UserName);
        //}

        //protected virtual void CheckErrors(IdentityResult identityResult)
        //{
        //    identityResult.CheckErrors(LocalizationManager);
        //}
    }
}
