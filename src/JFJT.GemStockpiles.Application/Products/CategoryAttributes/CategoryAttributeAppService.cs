using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Abp.UI;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.IdentityFramework;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Helpers;
using JFJT.GemStockpiles.Commons.Dto;
using JFJT.GemStockpiles.Models.Products;
using JFJT.GemStockpiles.Products.Categorys.Dto;
using JFJT.GemStockpiles.Products.CategoryAttributes.Dto;

namespace JFJT.GemStockpiles.Products.CategoryAttributes
{
    public class CategoryAttributeAppService : AsyncCrudAppService<CategoryAttribute, CategoryAttributeDto, Guid, PagedResultRequestExtendDto, CategoryAttributeDto, CategoryAttributeDto>, ICategoryAttributeAppService
    {
        private readonly IRepository<CategoryAttribute, Guid> _categoryAttributRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CategoryAttributeAppService(IRepository<CategoryAttribute, Guid> categoryAttributRepository,
            IRepository<Category, Guid> categoryRepository)
         : base(categoryAttributRepository)
        {
            _categoryAttributRepository = categoryAttributRepository;
            _categoryRepository = categoryRepository;
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

        public Task<ListResultDto<CategoryAttributeDto>> GetAttr(Guid Id)
        {
            var entity = _categoryAttributRepository.GetAllList().Where(a => a.CategoryId == Id);

            return Task.FromResult(new ListResultDto<CategoryAttributeDto>(
                ObjectMapper.Map<List<CategoryAttributeDto>>(entity)
            ));
        }

        public override async Task<PagedResultDto<CategoryAttributeDto>> GetAll(PagedResultRequestExtendDto input)
        {
            PagedResultDto<CategoryAttributeDto> query = await base.GetAll(input);

            //分类名称处理
            if (query.TotalCount > 0)
            {
                query.Items.ToList().ForEach(x =>
                {
                    x.CategoryName = _categoryRepository.FirstOrDefault(x.CategoryId).Name;
                });
            }

            return query;
        }

        protected override IQueryable<CategoryAttribute> CreateFilteredQuery(PagedResultRequestExtendDto input)
        {
            return Repository.GetAll().WhereIf(!input.KeyWord.IsNullOrWhiteSpace(), x => x.CategoryId.Equals(Guid.Parse(input.KeyWord)));
        }

        protected override IQueryable<CategoryAttribute> ApplySorting(IQueryable<CategoryAttribute> query, PagedResultRequestExtendDto input)
        {
            return query.OrderBy(r => r.CategoryId).ThenBy(r => r.Name);
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
