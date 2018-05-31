using System;

namespace JFJT.GemStockpiles.Enums
{
    /// <summary>
    /// 产品审核状态
    /// </summary>
    public enum ProductAuditStateEnum
    {
        /// <summary>
        /// 编辑待提交
        /// </summary>
        Editing = 10,
        /// <summary>
        /// 审核中
        /// </summary>
        Submitted = 20,
        /// <summary>
        /// 审核不通过
        /// </summary>
        Rejected = 30,
        /// <summary>
        /// 审核通过
        /// </summary>
        Approved = 40
    }
}
