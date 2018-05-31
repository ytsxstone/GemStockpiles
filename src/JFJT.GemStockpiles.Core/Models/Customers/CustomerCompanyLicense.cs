using System;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Entities;

namespace JFJT.GemStockpiles.Models.Customers
{
    /// <summary>
    /// 公司三证信息表
    /// </summary>
    public class CustomerCompanyLicense : FullAudited<Guid>
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 证书类型
        /// </summary>
        public CompanyLicenseTypeEnum Type { get; set; } = CompanyLicenseTypeEnum.BusinessLicense;

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
