using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Points.PointRanks.Dto;

namespace JFJT.GemStockpiles.Points.PointRanks
{
    public interface IPointRankAppService : IAsyncCrudAppService<PointRankDto, int, PagedResultRequestDto, PointRankDto, PointRankDto>
    {
        /// <summary>
        /// 获取所有积分等级列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<PointRankDto>> GetAllPointRanks();
    }
}
