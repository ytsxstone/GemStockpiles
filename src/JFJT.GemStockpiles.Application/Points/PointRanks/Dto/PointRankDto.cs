using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Models.Points;

namespace JFJT.GemStockpiles.Points.PointRanks.Dto
{
    /// <summary>
    /// 积分等级DTO
    /// </summary>
    [AutoMap(typeof(PointRank))]
    public class PointRankDto : EntityDto
    {
        /// <summary>
        /// 等级名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 等级头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 该等级的最小积分
        /// </summary>
        public int MinPoint { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
