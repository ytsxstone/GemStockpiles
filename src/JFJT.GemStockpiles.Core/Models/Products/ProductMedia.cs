using System;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Entities;

namespace JFJT.GemStockpiles.Models.Products
{
    /// <summary>
    /// 产品媒体信息表
    /// </summary>
    public class ProductMedia : CreateAudited
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 媒体类型
        /// </summary>
        public MediaTypeEnum Type { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
