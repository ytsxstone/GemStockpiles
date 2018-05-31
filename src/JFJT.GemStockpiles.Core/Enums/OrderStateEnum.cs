using System;

namespace JFJT.GemStockpiles.Enums
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStateEnum
    {
        /// <summary>
        /// 等待付款
        /// </summary>
        Paying = 10,
        /// <summary>
        /// 已付款
        /// </summary>
        Payed = 20,
        /// <summary>
        /// 已发货
        /// </summary>
        Sended = 30,
        /// <summary>
        /// 已收货
        /// </summary>
        Received = 40,
        /// <summary>
        /// 已完成 
        /// </summary>
        Completed = 50,
        /// <summary>
        /// 退款中
        /// </summary>
        Refunding = 60,
        /// <summary>
        /// 已退款 
        /// </summary>
        Refunded = 70,
        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 80
    }
}
