using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Products.CategoryAttributeDetails.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFJT.GemStockpiles.Products.CategoryAttributeDetails
{
    public interface ICategoryAttrDetailAppService : IAsyncCrudAppService<CategoryAttrDetailDto, Guid, PagedResultRequestDto, CategoryAttrDetailDto, CategoryAttrDetailDto>
    {
    }
}
