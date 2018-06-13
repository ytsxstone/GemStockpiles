using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Abp.UI;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.IdentityFramework;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Helpers;
using JFJT.GemStockpiles.Commons.Dto;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Products;
using JFJT.GemStockpiles.Products.Categorys.Dto;
using JFJT.GemStockpiles.Products.CategoryAttributes.Dto;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Entities;

namespace JFJT.GemStockpiles.Products.CategoryAttributes
{
    [AbpAuthorize(PermissionNames.Pages_ProductManagement_CategoryAttributes)]
    public class CategoryAttributeAppService : AsyncCrudAppService<CategoryAttribute, CategoryAttributeDto, Guid, PagedResultRequestExtendDto, CategoryAttributeDto, CategoryAttributeDto>, ICategoryAttributeAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<CategoryAttribute, Guid> _categoryAttributRepository;
        private readonly IRepository<CategoryAttributeItem, Guid> _categoryAttributeItemRepository;

        public CategoryAttributeAppService(
            IRepository<CategoryAttribute, Guid> categoryAttributRepository,
            IRepository<Category, Guid> categoryRepository,
            IRepository<CategoryAttributeItem, Guid> categoryAttributeItemRepository)
         : base(categoryAttributRepository)
        {
            _categoryAttributRepository = categoryAttributRepository;
            _categoryRepository = categoryRepository;
            _categoryAttributeItemRepository = categoryAttributeItemRepository;
        }

        /// <summary>
        /// 添加分类属性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_ProductManagement_CategoryAttributes_Create)]
        public override async Task<CategoryAttributeDto> Create(CategoryAttributeDto input)
        {
            CheckCreatePermission();

            CheckErrors(await CheckNameOrSortAsync(input.Id, input.CategoryId, input.Name, input.Sort));

            var entity = ObjectMapper.Map<CategoryAttribute>(input);

            entity = await _categoryAttributRepository.InsertAsync(entity);

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 修改分类属性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_ProductManagement_CategoryAttributes_Edit)]
        public override async Task<CategoryAttributeDto> Update(CategoryAttributeDto input)
        {
            CheckUpdatePermission();

            var entity = await _categoryAttributRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(CategoryAttribute), input.Id);

            CheckErrors(await CheckNameOrSortAsync(input.Id, input.CategoryId, input.Name, input.Sort));

            MapToEntity(input, entity);

            entity = await _categoryAttributRepository.UpdateAsync(entity);

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 删除分类属性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_ProductManagement_CategoryAttributes_Delete)]
        public override async Task Delete(EntityDto<Guid> input)
        {
            CheckDeletePermission();

            var entity = await _categoryAttributRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(CategoryAttribute), input.Id);

            await _categoryAttributRepository.DeleteAsync(entity);
            //删除属性值
            await _categoryAttributeItemRepository.DeleteAsync(t => t.CategoryAttributeId == input.Id);
        }

        /// <summary>
        /// 根据ID获取编辑属性信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CategoryAttributeDto> GetAttributeForEdit(Guid id)
        {
            var entity = await _categoryAttributRepository.FirstOrDefaultAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(CategoryAttribute), id);

            CategoryAttributeDto editAttributeDto = ObjectMapper.Map<CategoryAttributeDto>(entity);

            //获取分类数据
            var categoryData = _categoryRepository.GetAll().ToList();

            //设置属性分类信息
            var model = HandleCategoryInfo(categoryData, new CategoryAttributeDto() { CategoryId = entity.CategoryId });
            editAttributeDto.CategoryName = categoryData.Find(t => t.Id == entity.CategoryId)?.Name;
            editAttributeDto.CategoryNamePath = model.CategoryNamePath;
            editAttributeDto.CategoryIdPath = model.CategoryIdPath;

            return editAttributeDto;
        }

        /// <summary>
        /// 获取属性类型列表
        /// </summary>
        /// <returns></returns>
        public Task<ListResultDto<IdAndNameDto>> GetAllAttributeTypes()
        {
            List<IdAndNameDto> attributeTypes = new List<IdAndNameDto>();

            foreach (CategoryAttributeTypeEnum item in Enum.GetValues(typeof(CategoryAttributeTypeEnum)))
            {
                string desc = EnumHelper.GetEnumDescription(typeof(CategoryAttributeTypeEnum), (int)item);
                attributeTypes.Add(new IdAndNameDto() { Id = (int)item, Name = desc });
            }

            return Task.FromResult(new ListResultDto<IdAndNameDto>(
                ObjectMapper.Map<List<IdAndNameDto>>(attributeTypes)
            ));
        }

        /// <summary>
        /// 获取分类树形结构数据
        /// </summary>
        /// <returns></returns>
        public Task<ListResultDto<CategoryCascaderDto>> GetAllAttr()
        {
            List<CategoryCascaderDto> listData = new List<CategoryCascaderDto>();

            var entity = _categoryAttributRepository.GetAllList();

            if (entity != null && entity.Count > 0)
            {
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
        /// 根据分类ID获取属性列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<ListResultDto<CategoryAttributeDto>> GetAttr(Guid Id)
        {
            var entity = _categoryAttributRepository.GetAllList().Where(a => a.CategoryId == Id);

            return Task.FromResult(new ListResultDto<CategoryAttributeDto>(
                ObjectMapper.Map<List<CategoryAttributeDto>>(entity)
            ));
        }

        /// <summary>
        /// 重写基类的GetAll方法, 以处理属性分类名称、层级信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<CategoryAttributeDto>> GetAll(PagedResultRequestExtendDto input)
        {
            PagedResultDto<CategoryAttributeDto> query = await base.GetAll(input);

            var categoryData = _categoryRepository.GetAll().ToList();

            //分类名称、层级信息处理
            if (query.TotalCount > 0)
            {
                query.Items.ToList().ForEach(x =>
                {
                    var model = HandleCategoryInfo(categoryData, new CategoryAttributeDto() { CategoryId = x.CategoryId });
                    x.CategoryName = categoryData.Find(t => t.Id == x.CategoryId)?.Name;
                    x.CategoryNamePath = model.CategoryNamePath;
                    x.CategoryIdPath = model.CategoryIdPath;
                });
            }

            return query;
        }

        /// <summary>
        /// 处理属性分类层级信息
        /// </summary>
        /// <param name="categoryData"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected CategoryAttributeDto HandleCategoryInfo(List<Category> categoryData, CategoryAttributeDto model)
        {
            var category = categoryData.Find(t => t.Id == model.CategoryId);
            if (category != null)
            {
                if (category.ParentId != null)
                {
                    model.CategoryId = (Guid)category.ParentId;
                    HandleCategoryInfo(categoryData, model);
                }

                model.CategoryIdPath.Add(category.Id.ToString());
                model.CategoryNamePath.Add(category.Name);
            }

            return model;
        }

        /// <summary>
        /// Dto模型映射
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pointRank"></param>
        protected override void MapToEntity(CategoryAttributeDto input, CategoryAttribute categoryAttribute)
        {
            ObjectMapper.Map(input, categoryAttribute);
        }

        /// <summary>
        /// GetAll查询过滤条件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<CategoryAttribute> CreateFilteredQuery(PagedResultRequestExtendDto input)
        {
            return Repository.GetAll().WhereIf(!input.KeyWord.IsNullOrWhiteSpace(), x => x.CategoryId.Equals(Guid.Parse(input.KeyWord)));
        }

        /// <summary>
        /// GetAll查询排序条件
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<CategoryAttribute> ApplySorting(IQueryable<CategoryAttribute> query, PagedResultRequestExtendDto input)
        {
            return query.OrderBy(r => r.CategoryId).ThenBy(r => r.Sort);
        }

        /// <summary>
        /// 检测分类属性名称或属性排序值是否已存在
        /// </summary>
        /// <param name="expectedId"></param>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        protected async Task<IdentityResult> CheckNameOrSortAsync(Guid? expectedId, Guid categoryId, string name, int sort)
        {
            var entity = await _categoryAttributRepository.FirstOrDefaultAsync(b => b.CategoryId == categoryId && b.Name == name);
            if (entity != null && entity.Id != expectedId)
            {
                throw new UserFriendlyException("属性名称已存在");
            }

            entity = await _categoryAttributRepository.FirstOrDefaultAsync(b => b.CategoryId == categoryId && b.Sort == sort);
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
