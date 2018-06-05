using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Products;

namespace JFJT.GemStockpiles.Products.Analysis
{
    [AbpAuthorize(PermissionNames.Pages_ProductManagement_Analysis)]
    public class AnalysisAppService : IAnalysisAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public AnalysisAppService(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }
    }
}