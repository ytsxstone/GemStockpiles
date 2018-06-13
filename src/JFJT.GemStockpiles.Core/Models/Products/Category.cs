using System;
using System.Collections.Generic;
using JFJT.GemStockpiles.Entities;

namespace JFJT.GemStockpiles.Models.Products
{
    /// <summary>
    /// 产品分类表
    /// </summary>
    public class Category : FullAudited<Guid>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 分类属性列表
        /// </summary>
        public virtual List<CategoryAttribute> Items { get; set; } = new List<CategoryAttribute>();
    }
}
