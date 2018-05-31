using System;
using JFJT.GemStockpiles.Entities;

namespace JFJT.GemStockpiles.Models.Points
{
    /// <summary>
    /// 积分等级
    /// </summary>
    public class PointRank : CreateModifyAudited
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
