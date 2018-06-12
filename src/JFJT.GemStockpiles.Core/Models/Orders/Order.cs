using System;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Entities;

namespace JFJT.GemStockpiles.Models.Orders
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class Order : FullAudited<Guid>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderSn { get; set; }

        /// <summary>
        /// 订单重量
        /// </summary>
        public int OrderWeight { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public int OrderAmount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStateEnum OrderState { get; set; } = OrderStateEnum.Paying;

        /// <summary>
        /// 订单时间
        /// </summary>
        private DateTime OrderTime { get; set; } = DateTime.Now;

        #region 买家信息
        /// <summary>
        /// 买家Id
        /// </summary>
        public string BuyerId { get; set; }

        /// <summary>
        /// 买家用户名
        /// </summary>
        public string BuyerUserName { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 收货地区ID
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string BuyerRemark { get; set; }
        #endregion

        #region 卖家信息
        /// <summary>
        /// 卖家Id
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 卖家用户名
        /// </summary>
        public string SellerUserName { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Supplier { get; set; }
        #endregion

        #region 支付信息
        /// <summary>
        /// 支付方式
        /// </summary>
        public PayModeEnum PayMode { get; set; } = PayModeEnum.OfflinePay;

        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PaySn { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }
        #endregion

        #region 配送信息
        /// <summary>
        /// 配送单号
        /// </summary>
        public string ShipSn { get; set; }

        /// <summary>
        /// 配送公司
        /// </summary>
        public string ShipCompany { get; set; }

        /// <summary>
        /// 配送时间
        /// </summary>
        public DateTime ShipTime { get; set; }
        #endregion
    }
}
