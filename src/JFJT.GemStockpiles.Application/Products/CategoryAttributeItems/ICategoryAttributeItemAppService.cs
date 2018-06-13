using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Products.CategoryAttributeItems.Dto;

namespace JFJT.GemStockpiles.Products.CategoryAttributeItems
{
    public interface ICategoryAttributeItemAppService : IAsyncCrudAppService<CategoryAttributeItemDto, Guid, PagedResultRequestDto, CategoryAttributeItemDto, CategoryAttributeItemDto>
    {
        /// <summary>
        /// 根据属性ID获取属性值列表数据
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        Task<ListResultDto<CategoryAttributeItemDto>> GetCategoryAttributeItems(Guid attributeId);
    }
}
