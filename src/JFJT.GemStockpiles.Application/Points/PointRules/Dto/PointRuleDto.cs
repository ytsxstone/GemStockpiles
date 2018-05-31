using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Models.Points;

namespace JFJT.GemStockpiles.Points.PointRules.Dto
{
    /// <summary>
    /// 积分规则DTO
    /// </summary>
    [AutoMap(typeof(PointRule))]
    public class PointRuleDto : EntityDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public PointActionEnum Name { get; set; } = PointActionEnum.Upload;

        /// <summary>
        /// 奖励分值
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否活动兑换
        /// </summary>
        public bool IsActivity { get; set; } = false;

        /// <summary>
        /// 是否兑换商品
        /// </summary>
        public bool IsCommodity { get; set; } = false;
    }
}
