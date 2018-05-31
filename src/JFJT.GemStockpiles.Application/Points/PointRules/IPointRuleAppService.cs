using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Points.PointRules.Dto;

namespace JFJT.GemStockpiles.Points.PointRules
{
    public interface IPointRuleAppService : IAsyncCrudAppService<PointRuleDto, int, PagedResultRequestDto, PointRuleDto, PointRuleDto>
    {
        /// <summary>
        /// 获取所有积分规则列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<PointRuleDto>> GetAllPointRules();
    }
}
