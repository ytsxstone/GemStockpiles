using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Models.Products;

namespace JFJT.GemStockpiles.Products.CategoryAttributeItems.Dto
{
    [AutoMap(typeof(CategoryAttributeItem))]
    public class CategoryAttributeItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        [Required]
        public Guid CategoryAttributeId { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }
    }
}
