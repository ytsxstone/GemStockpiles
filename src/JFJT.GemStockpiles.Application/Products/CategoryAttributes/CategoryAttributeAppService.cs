using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.IdentityFramework;
using Abp.UI;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Helpers;
using JFJT.GemStockpiles.Commons.Dto;
using JFJT.GemStockpiles.Models.Products;
using JFJT.GemStockpiles.Products.CategoryAttributes.Dto;
using Microsoft.AspNetCore.Identity;
using JFJT.GemStockpiles.Products.Category.Dto;

namespace JFJT.GemStockpiles.Products.CategoryAttributes
{
    public class CategoryAttributeAppService : AsyncCrudAppService<CategoryAttribute, CategoryAttributeDto, Guid, PagedResultRequestDto, CategoryAttributeDto, CategoryAttributeDto>, ICategoryAttributeAppService
    {
        private readonly IRepository<CategoryAttribute, Guid> _categoryAttributRepository;

        public CategoryAttributeAppService(IRepository<CategoryAttribute, Guid> categoryAttributRepository)
         : base(categoryAttributRepository)
        {
            _categoryAttributRepository = categoryAttributRepository;
        }

        public Task<ListResultDto<CategoryCascaderDto>> GetAllAttr()
        {
            List<CategoryCascaderDto> listData = new List<CategoryCascaderDto>();
            var entity = _categoryAttributRepository.GetAllList();
            if (entity != null && entity.Count > 0) {
                foreach (var item in entity)
                {
                    var model = new CategoryCascaderDto() { label = item.Name, value = item.Id };
                    listData.Add(model);
                }
            }
            return Task.FromResult(new ListResultDto<CategoryCascaderDto>(
                ObjectMapper.Map<List<CategoryCascaderDto>>(listData)
            ));
        }

        /// <summary>
        /// 获得产品属性类型列表
        /// </summary>
        /// <returns></returns>
        public Task<ListResultDto<IdAndNameDto>> GetAllAttributeType()
        {
            List<IdAndNameDto> actions = new List<IdAndNameDto>();

            foreach (CategoryAttributeTypeEnum item in Enum.GetValues(typeof(CategoryAttributeTypeEnum)))
            {
                string desc = EnumHelper.GetEnumDescription(typeof(CategoryAttributeTypeEnum), (int)item);
                actions.Add(new IdAndNameDto() { Id = (int)item, Name = desc });
            }

            return Task.FromResult(new ListResultDto<IdAndNameDto>(
                ObjectMapper.Map<List<IdAndNameDto>>(actions)
            ));
        }

        public override async Task<CategoryAttributeDto> Create(CategoryAttributeDto input)
        {
            CheckCreatePermission();

            CheckErrors(await CheckParAsync(input.Id, input.Name,input.Sort));

            var entity = ObjectMapper.Map<CategoryAttribute>(input);

            entity = await _categoryAttributRepository.InsertAsync(entity);

            return MapToEntityDto(entity);
        }

        protected async Task<IdentityResult> CheckParAsync(Guid? expectedId, string name, int sort)
        {
            var entity = await _categoryAttributRepository.FirstOrDefaultAsync(b => b.Name == name);
            if (entity != null && entity.Id != expectedId)
            {
                throw new UserFriendlyException(name + " 属性名称已存在");
            }

            entity = await _categoryAttributRepository.FirstOrDefaultAsync(b => b.Sort == sort);
            if (entity != null && entity.Id != expectedId)
            {
                throw new UserFriendlyException(sort + "排序值已存在");
            }

            return IdentityResult.Success;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
