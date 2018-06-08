using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Products;
using JFJT.GemStockpiles.Products.Category.Dto;
using System.Linq;
using Abp.UI;
using Abp.Domain.Entities;

namespace JFJT.GemStockpiles.Products.Category
{
    [AbpAuthorize(PermissionNames.Pages_ProductManagement_Categorys)]
    public class CategoryAppService : AsyncCrudAppService<Categorys, CategoryDto, Guid, PagedResultRequestDto, CategoryDto, CategoryDto>, ICategoryAppService
    {
        private readonly IRepository<Categorys, Guid> _categoryRepository;

        public CategoryAppService(IRepository<Categorys, Guid> categoryRepository)
         : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_ProductManagement_Categorys_Create)]
        public override async Task<CategoryDto> Create(CategoryDto input)
        {
            CheckCreatePermission();

            if (_categoryRepository.GetAll().FirstOrDefault(b => b.Name == input.Name && b.ParentId == input.ParentId) != null)
                throw new UserFriendlyException(input.Name + " 分类名称已存在");

            if (_categoryRepository.GetAll().FirstOrDefault(b => b.Sort == input.Sort && b.ParentId == input.ParentId) != null)
                throw new UserFriendlyException(input.Sort + " 当前排序已存在");

            var entity = ObjectMapper.Map<Categorys>(input);

            if (input.ParentId != null) {



            }


            entity = await _categoryRepository.InsertAsync(entity);

            return MapToEntityDto(entity);
        }


        [AbpAuthorize(PermissionNames.Pages_ProductManagement_Categorys_View)]
        public Task<ListResultDto<CategoryDto>> GetParent()
        {
            var entity = _categoryRepository.GetAllList().Where(a => a.ParentId == null);
            return Task.FromResult(new ListResultDto<CategoryDto>(
                ObjectMapper.Map<List<CategoryDto>>(entity)
            ));
        }

        #region Tree
        public Task<ListResultDto<CategoryTreeDto>> GetTreeCategory()
        {
            List<CategoryTreeDto> treeList = new List<CategoryTreeDto>();

            var Category = _categoryRepository.GetAllList();
            var treeData = new ListResultDto<Categorys>(ObjectMapper.Map<List<Categorys>>(Category));

            if (treeData != null)
            {
                treeList = GetTreePermissionList(treeData,null, 0);
            }

            return Task.FromResult(new ListResultDto<CategoryTreeDto>(ObjectMapper.Map<List<CategoryTreeDto>>(treeList)));
        }

        /// <summary>
        /// 递归生成分类tree
        /// </summary>
        /// <param name="categoryData"></param>
        /// <param name="parentId"></param>
        /// <param name="parentLevel"></param>
        /// <returns></returns>
        private List<CategoryTreeDto> GetTreePermissionList(ListResultDto<Categorys> categoryData, Guid? parentId, int parentLevel)
        {
            List<CategoryTreeDto> treeList = new List<CategoryTreeDto>();

            var level = parentLevel + 1;
            var treeData = categoryData.Items.Where(b => b.ParentId == parentId).ToList();

            foreach (var item in treeData)
            {
                var children = GetTreePermissionList(categoryData, item.Id, level);
                var model = new CategoryTreeDto() { title = item.Name, level = level };
                model.children = children.Count <= 0 ? null : children;
                model.expand = level <= 2 ? true : false;

                treeList.Add(model);
            }
            return treeList;
        }
        #endregion

        #region Cascader
        public Task<ListResultDto<CategoryCascaderDto>> GetCascaderCategory()
        {
            List<CategoryCascaderDto> treeList = new List<CategoryCascaderDto>();

            var Category = _categoryRepository.GetAllList();
            var treeData = new ListResultDto<Categorys>(ObjectMapper.Map<List<Categorys>>(Category));

            if (treeData != null)
            {
                treeList = GetCascaderPermissionList(treeData, null, 0);
            }

            return Task.FromResult(new ListResultDto<CategoryCascaderDto>(ObjectMapper.Map<List<CategoryCascaderDto>>(treeList)));
        }

        /// <summary>
        /// 递归生成分类Cascader
        /// </summary>
        /// <param name="categoryData"></param>
        /// <param name="parentId"></param>
        /// <param name="parentLevel"></param>
        /// <returns></returns>
        private List<CategoryCascaderDto> GetCascaderPermissionList(ListResultDto<Categorys> categoryData, Guid? parentId, int parentLevel)
        {
            List<CategoryCascaderDto> treeList = new List<CategoryCascaderDto>();

            var level = parentLevel + 1;
            var treeData = categoryData.Items.Where(b => b.ParentId == parentId).ToList();

            foreach (var item in treeData)
            {
                var children = GetCascaderPermissionList(categoryData, item.Id, level);
                var model = new CategoryCascaderDto() { label = item.Name, value = item.Id };
                model.children = children.Count <= 0 ? null : children;

                treeList.Add(model);
            }
            return treeList;
        }
        #endregion
    }
}