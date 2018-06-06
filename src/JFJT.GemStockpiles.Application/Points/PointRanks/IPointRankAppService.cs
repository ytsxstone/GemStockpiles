using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Points.PointRanks.Dto;

namespace JFJT.GemStockpiles.Points.PointRanks
{
    public interface IPointRankAppService : IAsyncCrudAppService<PointRankDto, Guid, PagedResultRequestDto, PointRankDto, PointRankDto>
    {

    }
}
