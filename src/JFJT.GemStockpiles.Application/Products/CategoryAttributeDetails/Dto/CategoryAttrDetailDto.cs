using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JFJT.GemStockpiles.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFJT.GemStockpiles.Products.CategoryAttributeDetails.Dto
{
    [AutoMap(typeof(CategoryAttributeItem))]
    public class CategoryAttrDetailDto: EntityDto<Guid>
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
