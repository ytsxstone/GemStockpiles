using System;
using System.Reflection;
using System.ComponentModel;

namespace JFJT.GemStockpiles.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举变量的描述信息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Type e, int? value)
        {
            FieldInfo[] fields = e.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                if ((int)System.Enum.Parse(e, fields[i].Name) == value)
                {
                    DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])fields[i].
                    GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (EnumAttributes.Length > 0)
                    {
                        return EnumAttributes[0].Description;
                    }
                }
            }
            return "没有描述信息";
        }
    }
}