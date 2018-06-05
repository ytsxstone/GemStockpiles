using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Points.PointRules.Dto;

namespace JFJT.GemStockpiles.Points.PointRules
{
    public interface IPointRuleAppService : IAsyncCrudAppService<PointRuleDto, Guid, PagedResultRequestDto, PointRuleDto, PointRuleDto>
    {
        Task<ListResultDto<PointActionDto>> GetAllPointActions();
    }
}
