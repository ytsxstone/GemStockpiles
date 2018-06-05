using System;
using System.Collections.Generic;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Models.Products;

namespace JFJT.GemStockpiles.Products.Products.Dto
{
    /// <summary>
    /// 商品管理Dto
    /// </summary>
    [AutoMap(typeof(Product))]
    public class ProductDto: EntityDto<Guid>
    {
        #region 基础字段

        /// <summary>
        /// 分类Id
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 证书号
        /// </summary>
        public string CertNo { get; set; }

        /// <summary>
        /// 重量(毫克)
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 价格(分)
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 产品备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 产品属性列表
        /// </summary>
        public virtual List<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();

        /// <summary>
        /// 产品媒体列表
        /// </summary>
        public virtual List<ProductMedia> ProductMedias { get; set; } = new List<ProductMedia>();

        #endregion

        #region 应用字段

        /// <summary>
        /// 所属客户Id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 产品审核状态
        /// </summary>
        public ProductAuditStateEnum AuditState { get; set; } = ProductAuditStateEnum.Editing;

        /// <summary>
        /// 产品销售状态
        /// </summary>
        public ProductSaleStateEnum SaleState { get; set; } = ProductSaleStateEnum.OffShelves;

        /// <summary>
        /// 是否推荐商品
        /// </summary>
        public bool IsRecommend { get; set; } = false;

        /// <summary>
        /// 审核人登录帐号
        /// </summary>
        public string AuditedAccount { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditedTime { get; set; }

        /// <summary>
        /// 驳回原因
        /// </summary>
        public string RejectedRemark { get; set; }

        #endregion
    }
}
