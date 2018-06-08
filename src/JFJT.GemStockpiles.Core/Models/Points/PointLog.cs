using System;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Entities;

namespace JFJT.GemStockpiles.Models.Points
{
    /// <summary>
    /// 平台会员积分日志
    /// </summary>
    public class PointLog : CreateAudited<Guid>
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 积分值
        /// </summary>
        public int Points { get; set; }

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
