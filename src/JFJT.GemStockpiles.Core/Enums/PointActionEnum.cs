using System;
using System.ComponentModel;

namespace JFJT.GemStockpiles.Enums
{
    /// <summary>
    /// 积分动作
    /// </summary>
    public enum PointActionEnum
    {
        /// <summary>
        /// 上传商品
        /// </summary>
        [Description("上传商品")]
        Upload = 10,
        /// <summary>
        /// 购买商品
        /// </summary>
        [Description("购买商品")]
        Buy = 20,
        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册")]
        Register = 30,
        /// <summary>
        /// 推荐
        /// </summary>
        [Description("推荐")]
        Recommend = 40
    }
}
