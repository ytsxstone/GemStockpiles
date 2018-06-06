using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Abp.UI;
using AutoMapper;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
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
            List<PointActionDto> actions = this.GetPointActions();

            return Task.FromResult(new ListResultDto<PointActionDto>(
                ObjectMapper.Map<List<PointActionDto>>(actions)
            ));
        }

        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRules_Create)]
        public override async Task<PointRuleDto> Create(PointRuleDto input)
        {
            CheckCreatePermission();

            CheckErrors(await CheckActionNameAsync(input.Id, input.Name));

            var entity = ObjectMapper.Map<PointRule>(input);

            entity = await _pointRuleRepository.InsertAsync(entity);

            return MapToEntityDto(entity);
        }

        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRules_Edit)]
        public override async Task<PointRuleDto> Update(PointRuleDto input)
        {
            CheckUpdatePermission();

            var entity = await _pointRuleRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(PointRule), input.Id);

            CheckErrors(await CheckActionNameAsync(input.Id, input.Name));

            MapToEntity(input, entity);

            entity = await _pointRuleRepository.UpdateAsync(entity);

            return MapToEntityDto(entity);
        }

        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRules_Delete)]
        public override async Task Delete(EntityDto<Guid> input)
        {
            CheckDeletePermission();

            var entity = await _pointRuleRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(PointRank), input.Id);

            await _pointRuleRepository.DeleteAsync(entity);
        }

        protected override void MapToEntity(PointRuleDto input, PointRule pointRule)
        {
            ObjectMapper.Map(input, pointRule);
        }

        protected override async Task<PointRule> GetEntityByIdAsync(Guid id)
        {
            var entity = await Repository.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(PointRank), id);
            }

            return entity;
        }

        protected override IQueryable<PointRule> ApplySorting(IQueryable<PointRule> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.Name);
        }

        public async Task<IdentityResult> CheckActionNameAsync(Guid? expectedId, PointActionEnum name)
        {
            var entity = await _pointRuleRepository.FirstOrDefaultAsync(b => b.Name == name);
            if (entity != null && entity.Id != expectedId)
            {
                throw new UserFriendlyException("积分方案已存在");
            }

            return IdentityResult.Success;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        /// <summary>
        /// 获取积分动作列表
        /// </summary>
        /// <returns></returns>
        protected List<PointActionDto> GetPointActions()
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

            return actions;
        }
    }
}
