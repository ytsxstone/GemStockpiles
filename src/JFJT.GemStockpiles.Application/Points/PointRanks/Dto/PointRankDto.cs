using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Models.Points;

namespace JFJT.GemStockpiles.Points.PointRanks.Dto
{
    /// <summary>
    /// 积分等级DTO
    /// </summary>
    [AutoMap(typeof(PointRank))]
    public class PointRankDto : EntityDto<Guid>
    {
        /// <summary>
        /// 等级名称
        /// </summary>
        [Required]
        [MaxLength(16, ErrorMessage = "最多输入16个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 等级头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 该等级的最小积分
        /// </summary>
        [Required]
        [Range(1, 99999999, ErrorMessage = "最小积分必须是1~99999999之间的整数")]
        public int MinPoint { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
