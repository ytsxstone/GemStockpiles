using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Abp.UI;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Helpers;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Points;
using JFJT.GemStockpiles.Commons.Dto;
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
        /// 添加积分方案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRules_Create)]
        public override async Task<PointRuleDto> Create(PointRuleDto input)
        {
            CheckCreatePermission();

            CheckErrors(await CheckActionNameAsync(input.Id, input.Name));

            var entity = ObjectMapper.Map<PointRule>(input);

            entity = await _pointRuleRepository.InsertAsync(entity);

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 修改积分方案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除积分方案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRules_Delete)]
        public override async Task Delete(EntityDto<Guid> input)
        {
            CheckDeletePermission();

            var entity = await _pointRuleRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(PointRank), input.Id);

            await _pointRuleRepository.DeleteAsync(entity);
        }

        /// <summary>
        /// 获取积分动作列表
        /// </summary>
        /// <returns></returns>
        public Task<ListResultDto<IdAndNameDto>> GetAllPointActions()
        {
            List<IdAndNameDto> actions = new List<IdAndNameDto>();

            foreach (PointActionEnum item in Enum.GetValues(typeof(PointActionEnum)))
            {
                string desc = EnumHelper.GetEnumDescription(typeof(PointActionEnum), (int)item);
                actions.Add(new IdAndNameDto() { Id = (int)item, Name = desc });
            }

            return Task.FromResult(new ListResultDto<IdAndNameDto>(
                ObjectMapper.Map<List<IdAndNameDto>>(actions)
            ));
        }

        /// <summary>
        /// Dto模型映射
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pointRule"></param>
        protected override void MapToEntity(PointRuleDto input, PointRule pointRule)
        {
            ObjectMapper.Map(input, pointRule);
        }

        /// <summary>
        /// 根据ID获取Entity模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected override async Task<PointRule> GetEntityByIdAsync(Guid id)
        {
            var entity = await Repository.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(PointRank), id);
            }

            return entity;
        }

        /// <summary>
        /// GetAll查询排序条件
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<PointRule> ApplySorting(IQueryable<PointRule> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.Name);
        }

        /// <summary>
        /// 检测积分方案名称是否已存在
        /// </summary>
        /// <param name="expectedId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected async Task<IdentityResult> CheckActionNameAsync(Guid? expectedId, PointActionEnum name)
        {
            var entity = await _pointRuleRepository.FirstOrDefaultAsync(b => b.Name == name);
            if (entity != null && entity.Id != expectedId)
            {
                throw new UserFriendlyException("积分方案已存在");
            }

            return IdentityResult.Success;
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
