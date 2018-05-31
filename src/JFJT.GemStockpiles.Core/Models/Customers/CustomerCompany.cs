using System;
using JFJT.GemStockpiles.Entities;

namespace JFJT.GemStockpiles.Models.Customers
{
    /// <summary>
    /// 客户公司信息表
    /// </summary>
    public class CustomerCompany : FullAudited<Guid>
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 公司注册时间
        /// </summary>
        public DateTime? RegisterTime { get; set; } = DateTime.Now;
    }
}
