﻿using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Products.Products.Dto;

namespace JFJT.GemStockpiles.Products.Products
{
    public interface IAnalysisAppService : IAsyncCrudAppService<ProductDto, Guid, PagedResultRequestDto, ProductDto, ProductDto>
    {

    }
}