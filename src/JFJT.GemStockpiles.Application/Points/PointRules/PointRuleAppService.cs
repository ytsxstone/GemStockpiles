using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Abp.Authorization;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Points;
using JFJT.GemStockpiles.Points.PointRules.Dto;

namespace JFJT.GemStockpiles.Points.PointRules
{
    [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRules)]
    public class PointRuleAppService : AsyncCrudAppService<PointRule, PointRuleDto, int, PagedResultRequestDto, PointRuleDto, PointRuleDto>, IPointRuleAppService
    {
        private readonly IRepository<PointRule> _pointRuleRepository;

        public PointRuleAppService(IRepository<PointRule> pointRuleRepository)
            : base(pointRuleRepository)
        {
            _pointRuleRepository = pointRuleRepository;
        }

        /// <summary>
        /// 获取所有积分规则列表
        /// </summary>
        /// <returns></returns>
        public Task<ListResultDto<PointRuleDto>> GetAllPointRules()
        {
            var pointRules = _pointRuleRepository.GetAllList();

            return Task.FromResult(new ListResultDto<PointRuleDto>(
                ObjectMapper.Map<List<PointRuleDto>>(pointRules)
            ));
        }
    }
}
