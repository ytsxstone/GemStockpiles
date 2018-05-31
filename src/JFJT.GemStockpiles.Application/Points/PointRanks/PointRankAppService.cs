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
using JFJT.GemStockpiles.Points.PointRanks.Dto;

namespace JFJT.GemStockpiles.Points.PointRanks
{
    [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRanks)]
    public class PointRankAppService : AsyncCrudAppService<PointRule, PointRankDto, int, PagedResultRequestDto, PointRankDto, PointRankDto>, IPointRankAppService
    {
        private readonly IRepository<PointRule> _pointRuleRepository;

        public PointRankAppService(IRepository<PointRule> pointRuleRepository)
            : base(pointRuleRepository)
        {
            _pointRuleRepository = pointRuleRepository;
        }

        /// <summary>
        /// 获取所有积分等级列表
        /// </summary>
        /// <returns></returns>
        public Task<ListResultDto<PointRankDto>> GetAllPointRanks()
        {
            var pointRules = _pointRuleRepository.GetAllList();

            return Task.FromResult(new ListResultDto<PointRankDto>(
                ObjectMapper.Map<List<PointRankDto>>(pointRules)
            ));
        }
    }
}
