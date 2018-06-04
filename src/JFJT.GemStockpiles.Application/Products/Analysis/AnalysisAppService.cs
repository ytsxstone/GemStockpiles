using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Products;
using JFJT.GemStockpiles.Products.Products.Dto;

namespace JFJT.GemStockpiles.Products.Products
{
    [AbpAuthorize(PermissionNames.Pages_ProductManagement_Analysis)]
    public class AnalysisAppService : AsyncCrudAppService<Product, ProductDto, Guid, PagedResultRequestDto, ProductDto, ProductDto>, IAnalysisAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public AnalysisAppService(IRepository<Product, Guid> productRepository)
         : base(productRepository)
        {
            _productRepository = productRepository;
        }
    }
}