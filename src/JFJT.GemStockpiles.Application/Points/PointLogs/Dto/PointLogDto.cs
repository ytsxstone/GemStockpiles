using System;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Models.Points;

namespace JFJT.GemStockpiles.Points.PointLogs.Dto
{
    /// <summary>
    /// 积分日志DTO
    /// </summary>
    [AutoMap(typeof(PointLog))]
    public class PointLogDto : EntityDto<Guid>
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 动作类型
        /// </summary>
        public PointActionEnum ActionType { get; set; } = PointActionEnum.Upload;

        /// <summary>
        /// 动作描述
        /// </summary>
        public string ActionDesc { get; set; }

        /// <summary>
        /// 动作时间
        /// </summary>
        public DateTime ActionTime { get; set; } = System.DateTime.Now;
    }
}
