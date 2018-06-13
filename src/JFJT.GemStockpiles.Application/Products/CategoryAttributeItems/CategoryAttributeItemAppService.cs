using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Abp.UI;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.IdentityFramework;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Products;
using JFJT.GemStockpiles.Products.CategoryAttributeItems.Dto;

namespace JFJT.GemStockpiles.Products.CategoryAttributeItems
{
    [AbpAuthorize(PermissionNames.Pages_ProductManagement_CategoryAttributes)]
    public class CategoryAttributeItemAppService : AsyncCrudAppService<CategoryAttributeItem, CategoryAttributeItemDto, Guid, PagedResultRequestDto, CategoryAttributeItemDto, CategoryAttributeItemDto>, ICategoryAttributeItemAppService
    {
        private readonly IRepository<CategoryAttributeItem, Guid> _categoryAttributeItemRepository;

        public CategoryAttributeItemAppService(IRepository<CategoryAttributeItem, Guid> categoryAttributeItemRepository)
         : base(categoryAttributeItemRepository)
        {
            _categoryAttributeItemRepository = categoryAttributeItemRepository;
        }

        /// <summary>
        /// 添加属性值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_ProductManagement_CategoryAttributes_Create)]
        public override async Task<CategoryAttributeItemDto> Create(CategoryAttributeItemDto input)
        {
            CheckCreatePermission();

            CheckErrors(await CheckValueOrSortAsync(input.Id, input.CategoryAttributeId, input.Value, input.Sort));

            var entity = ObjectMapper.Map<CategoryAttributeItem>(input);

            entity = await _categoryAttributeItemRepository.InsertAsync(entity);

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_ProductManagement_CategoryAttributes_Edit)]
        public override async Task<CategoryAttributeItemDto> Update(CategoryAttributeItemDto input)
        {
            CheckUpdatePermission();

            var entity = await _categoryAttributeItemRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(CategoryAttributeItem), input.Id);

            CheckErrors(await CheckValueOrSortAsync(input.Id, input.CategoryAttributeId, input.Value, input.Sort));

            MapToEntity(input, entity);

            entity = await _categoryAttributeItemRepository.UpdateAsync(entity);

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_ProductManagement_CategoryAttributes_Delete)]
        public override async Task Delete(EntityDto<Guid> input)
        {
            CheckDeletePermission();

            var entity = await _categoryAttributeItemRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(CategoryAttributeItem), input.Id);

            await _categoryAttributeItemRepository.DeleteAsync(entity);
        }

        /// <summary>
        /// 根据属性ID获取属性值列表数据
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public Task<ListResultDto<CategoryAttributeItemDto>> GetCategoryAttributeItems(Guid attributeId)
        {
            var entity = _categoryAttributeItemRepository.GetAll().Where(t => t.CategoryAttributeId == attributeId).OrderBy(t => t.Sort);

            return Task.FromResult(new ListResultDto<CategoryAttributeItemDto>(
                ObjectMapper.Map<List<CategoryAttributeItemDto>>(entity)
            ));
        }

        /// <summary>
        /// Dto模型映射
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pointRank"></param>
        protected override void MapToEntity(CategoryAttributeItemDto input, CategoryAttributeItem categoryAttributeItem)
        {
            ObjectMapper.Map(input, categoryAttributeItem);
        }

        /// <summary>
        /// 检测属性值或排序值是否已存在
        /// </summary>
        /// <param name="expectedId"></param>
        /// <param name="attributeId"></param>
        /// <param name="value"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        protected async Task<IdentityResult> CheckValueOrSortAsync(Guid? expectedId, Guid attributeId, string value, int sort)
        {
            var entity = await _categoryAttributeItemRepository.FirstOrDefaultAsync(b => b.CategoryAttributeId == attributeId && b.Value == value);
            if (entity != null && entity.Id != expectedId)
            {
                throw new UserFriendlyException("属性值已存在");
            }

            entity = await _categoryAttributeItemRepository.FirstOrDefaultAsync(b => b.CategoryAttributeId == attributeId && b.Sort == sort);
            if (entity != null && entity.Id != expectedId)
            {
                throw new UserFriendlyException("排序值已存在");
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
