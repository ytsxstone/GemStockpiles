using System;
using System.Collections.Generic;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
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
        /// 类型名称层级路径
        /// </summary>
        public List<string> CategoryNamePath { get; set; } = new List<string>();

        /// <summary>
        /// 类型ID
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 类型ID层级路径
        /// </summary>
        public List<string> CategoryIdPath { get; set; } = new List<string>();

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
