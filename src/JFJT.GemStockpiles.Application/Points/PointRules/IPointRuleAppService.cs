using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Commons.Dto;
using JFJT.GemStockpiles.Points.PointRules.Dto;

namespace JFJT.GemStockpiles.Points.PointRules
{
    public interface IPointRuleAppService : IAsyncCrudAppService<PointRuleDto, Guid, PagedResultRequestDto, PointRuleDto, PointRuleDto>
    {
        /// <summary>
        /// 获取积分动作列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<IdAndNameDto>> GetAllPointActions();
    }
}
