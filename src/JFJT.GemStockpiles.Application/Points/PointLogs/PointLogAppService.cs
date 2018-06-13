using System;
using Abp.Domain.Repositories;
using AutoMapper;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Helpers;
using JFJT.GemStockpiles.Models.Points;
using JFJT.GemStockpiles.Points.PointLogs.Dto;

namespace JFJT.GemStockpiles.Points.PointLogs
{
    public class PointLogAppService : GemStockpilesAppServiceBase
    {
        private readonly IRepository<PointLog, Guid> _pointLogRepository;
        private readonly IRepository<PointRule, Guid> _pointRuleRepository;

        public PointLogAppService(IRepository<PointLog, Guid> pointLogRepository, IRepository<PointRule, Guid> pointRuleRepository)
        {
            _pointLogRepository = pointLogRepository;
            _pointRuleRepository = pointRuleRepository;
        }

        /// <summary>
        /// 发放积分
        /// </summary>
        public async void SendPoints(PointLogDto input)
        {
            //获取积分规则
            var pointRule = await _pointRuleRepository.FirstOrDefaultAsync(b => b.Name == input.ActionType);
            if (pointRule == null)
            {
                return;
            }

            //获取规则默认描述
            string defaultDesc = EnumHelper.GetEnumDescription(typeof(PointActionEnum), (int)pointRule.Name);

            //对象映射
            var pointLog = ObjectMapper.Map<PointLog>(input);

            //默认值处理
            pointLog.Points = pointRule.Point;
            if (string.IsNullOrEmpty(pointLog.ActionDesc))
            {
                pointLog.ActionDesc = defaultDesc;
            }

            await _pointLogRepository.InsertAsync(pointLog);
        }
    }
}
