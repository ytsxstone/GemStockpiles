using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Models.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFJT.GemStockpiles.Products
{
    public interface IProductAppService : IAsyncCrudAppService<ProductDto, Guid, PagedResultRequestDto, ProductDto, ProductDto>
    {
        /// <summary>
        /// 获取所有积分等级列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<ProductDto>> GetAllProducts();
    }
}