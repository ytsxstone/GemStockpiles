using System;

namespace JFJT.GemStockpiles.Enums
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PayModeEnum
    {
        /// <summary>
        /// 线下付款
        /// </summary>
        OfflinePay = 10,
        /// <summary>
        /// 支付宝
        /// </summary>
        Alipay = 20,
        /// <summary>
        /// 微信
        /// </summary>
        WeChat = 30,
        /// <summary>
        /// 银联转账
        /// </summary>
        UnionPay = 40
    }
}
