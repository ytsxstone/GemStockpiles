using System;
using JFJT.GemStockpiles.Entities;

namespace JFJT.GemStockpiles.Models.Products
{
    /// <summary>
    /// 分类属性可选值表
    /// </summary>
    public class CategoryAttributeItem : CreateModifyAudited<Guid>
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        public Guid AttributeId { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }
    }
}
