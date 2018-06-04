using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFJT.GemStockpiles.Products
{
    [AbpAuthorize(PermissionNames.Pages_ProductManagement_Products)]
    public class ProductAppService : AsyncCrudAppService<Product, ProductDto, Guid, PagedResultRequestDto, ProductDto, ProductDto>, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductAppService(IRepository<Product, Guid> productRepository)
         : base(productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// 获取所有商品管理
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<ProductDto>> GetAllProducts()
        {
            var pointRules = await _productRepository.GetAllListAsync();

            return new ListResultDto<ProductDto>(ObjectMapper.Map<List<ProductDto>>(pointRules));
        }
    }
}