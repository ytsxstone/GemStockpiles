using System;

namespace JFJT.GemStockpiles.Enums
{
    /// <summary>
    /// 注册用户审核状态
    /// </summary>
    public enum UserStateEnum
    {
        /// <summary>
        /// 审核中
        /// </summary>
        Submited = 10,
        /// <summary>
        /// 审核不通过
        /// </summary>
        Rejected = 20,
        /// <summary>
        /// 审核通过
        /// </summary>
        Approved = 30
    }
}
