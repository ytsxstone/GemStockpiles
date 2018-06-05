using System;
using System.Collections.Generic;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Models.Products;

namespace JFJT.GemStockpiles.Products.Category.Dto
{
    [AutoMap(typeof(Categorys))]
    public class CategoryDto : EntityDto<Guid>
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


        //public List<CategoryDto> Childs { get; set; } = new List<CategoryDto>();
    }
}
