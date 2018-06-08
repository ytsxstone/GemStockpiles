using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Commons.Dto;
using JFJT.GemStockpiles.Products.Category.Dto;
using JFJT.GemStockpiles.Products.CategoryAttributes.Dto;

namespace JFJT.GemStockpiles.Products.CategoryAttributes
{
    public interface ICategoryAttributeAppService : IAsyncCrudAppService<CategoryAttributeDto, Guid, PagedResultRequestDto, CategoryAttributeDto, CategoryAttributeDto>
    {
        Task<ListResultDto<IdAndNameDto>> GetAllAttributeType();

        Task<ListResultDto<CategoryCascaderDto>> GetAllAttr();
    }
}
