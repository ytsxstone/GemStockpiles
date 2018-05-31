using System;

namespace JFJT.GemStockpiles.Enums
{
    /// <summary>
    /// 产品销售状态
    /// </summary>
    public enum ProductSaleStateEnum
    {
        /// <summary>
        /// 下架
        /// </summary>
        OffShelves = 10,
        /// <summary>
        /// 上架
        /// </summary>
        OnSale = 20,
        /// <summary>
        /// 卖完了
        /// </summary>
        SoldOut = 30
    }
}
