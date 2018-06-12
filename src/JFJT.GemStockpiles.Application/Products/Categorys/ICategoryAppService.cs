using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Products.Categorys.Dto;

namespace JFJT.GemStockpiles.Products.Categorys
{
    public interface ICategoryAppService : IAsyncCrudAppService<CategoryDto, Guid, PagedResultRequestDto, CategoryDto, CategoryDto>
    {
        Task<ListResultDto<CategoryDto>> GetParent();

        Task<ListResultDto<CategoryTreeDto>> GetTreeCategory();

        Task<ListResultDto<CategoryCascaderDto>> GetCascaderCategory();
    }
}