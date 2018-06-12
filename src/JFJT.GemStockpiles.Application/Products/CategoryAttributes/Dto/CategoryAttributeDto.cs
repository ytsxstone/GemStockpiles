using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Models.Products;

namespace JFJT.GemStockpiles.Products.CategoryAttributes.Dto
{
    [AutoMap(typeof(CategoryAttribute))]
    public class CategoryAttributeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 类型ID
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
