using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using JFJT.GemStockpiles.Models.Products;
using JFJT.GemStockpiles.Products.CategoryAttributeDetails.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace JFJT.GemStockpiles.Products.CategoryAttributeDetails
{
    public class CategoryAttrDetailAppService: AsyncCrudAppService<CategoryAttributeItem, CategoryAttrDetailDto, Guid, PagedResultRequestDto, CategoryAttrDetailDto, CategoryAttrDetailDto>, ICategoryAttrDetailAppService
    {
        private readonly IRepository<CategoryAttributeItem, Guid> _categoryAttrDetailRepository;

        public CategoryAttrDetailAppService(IRepository<CategoryAttributeItem, Guid> categoryAttrDetailRepository)
         : base(categoryAttrDetailRepository)
        {
            _categoryAttrDetailRepository = categoryAttrDetailRepository;
        }
        public override async Task<CategoryAttrDetailDto> Create(CategoryAttrDetailDto input)
        {
            CheckCreatePermission();

            var entity = ObjectMapper.Map<CategoryAttributeItem>(input);

            entity = await _categoryAttrDetailRepository.InsertAsync(entity);

            return MapToEntityDto(entity);
        }
        public Task<ListResultDto<CategoryAttrDetailDto>> GetAttr(Guid Id)
        {
            var entity = _categoryAttrDetailRepository.GetAllList().Where(a=>a.AttributeId==Id);
            return Task.FromResult(new ListResultDto<CategoryAttrDetailDto>(
                ObjectMapper.Map<List<CategoryAttrDetailDto>>(entity)
            ));
        }
    }
}
