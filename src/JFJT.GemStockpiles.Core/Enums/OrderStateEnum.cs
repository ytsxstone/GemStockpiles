using System;

namespace JFJT.GemStockpiles.Enums
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStateEnum
    {
        /// <summary>
        /// 待付款
        /// </summary>
        Paying = 10,
        /// <summary>
        /// 待确认
        /// </summary>
        Confirming = 20,
        /// <summary>
        /// 待发货
        /// </summary>
        Payed = 30,
        /// <summary>
        /// 待收货
        /// </summary>
        Sended = 40,
        /// <summary>
        /// 待评价
        /// </summary>
        Received = 50,
        /// <summary>
        /// 已完成 
        /// </summary>
        Completed = 60,
        /// <summary>
        /// 已关闭
        /// </summary>
        Cancelled = 70
    }
}
