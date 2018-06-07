using System;
using System.ComponentModel;

namespace JFJT.GemStockpiles.Enums
{
    /// <summary>
    /// 产品属性类型
    /// </summary>
    public enum CategoryAttributeTypeEnum
    {
        /// <summary>
        /// 文本框
        /// </summary>
        [Description("文本框")]
        TextBox =10,
        /// <summary>
        /// 下拉框
        /// </summary>
        [Description("下拉框")]
        DropDownList = 20,
        /// <summary>
        /// 单选框
        /// </summary>
        [Description("单选框")]
        RadioButton = 30,
        /// <summary>
        /// 多选框
        /// </summary>
        [Description("多选框")]
        CheckBox = 40,
        /// <summary>
        /// 文本域
        /// </summary>
        [Description("文本域")]
        TextArea = 50
    }
}
