using System;
using Abp.Application.Services.Dto;

namespace JFJT.GemStockpiles.Common.Dto
{
    /// <summary>
    /// 分页结果请求扩展Dto
    /// </summary>
    public class PagedResultRequestExtendDto : PagedResultRequestDto
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public virtual string KeyWord { get; set; } = "";
    }
}
