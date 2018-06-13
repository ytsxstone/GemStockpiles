using System;
using System.Collections.Generic;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Entities;

namespace JFJT.GemStockpiles.Models.Products
{
    /// <summary>
    /// 产品分类属性表
    /// </summary>
    public class CategoryAttribute : FullAudited<Guid>
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类属性类型
        /// </summary>
        public CategoryAttributeTypeEnum Type { get; set; } = CategoryAttributeTypeEnum.TextBox;

        /// <summary>
        /// 是否必须
        /// </summary>
        public bool Required { get; set; } = false;

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 属性可选值列表
        /// </summary>
        public virtual List<CategoryAttributeItem> Items { get; set; } = new List<CategoryAttributeItem>();
    }
}
