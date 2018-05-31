using System;
using Abp.Application.Services.Dto;

namespace JFJT.GemStockpiles.Common.Dto
{
    /// <summary>
    /// 分页结果请求扩展Dto
    /// </summary>
    public class PagedResultRequestExtendDto : PagedResultRequestDto
    {
        int _skipCount = 1;
        /// <summary>
        /// 页码
        /// </summary>
        public override int SkipCount
        {
            get
            {
                return (_skipCount - 1) * this.MaxResultCount;
            }
            set
            {
                _skipCount = value;
            }
        }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public override int MaxResultCount
        {
            get => base.MaxResultCount;
            set => base.MaxResultCount = value;
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
    }
}
