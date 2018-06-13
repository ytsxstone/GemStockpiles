using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Commons.Dto;
using JFJT.GemStockpiles.Products.Categorys.Dto;
using JFJT.GemStockpiles.Products.CategoryAttributes.Dto;

namespace JFJT.GemStockpiles.Products.CategoryAttributes
{
    public interface ICategoryAttributeAppService : IAsyncCrudAppService<CategoryAttributeDto, Guid, PagedResultRequestExtendDto, CategoryAttributeDto, CategoryAttributeDto>
    {
        /// <summary>
        /// 获取属性类型列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<IdAndNameDto>> GetAllAttributeTypes();

        /// <summary>
        /// 获取分类树形结构数据
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<CategoryCascaderDto>> GetAllAttr();

        /// <summary>
        /// 根据分类ID获取属性列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ListResultDto<CategoryAttributeDto>> GetAttr(Guid Id);

        /// <summary>
        /// 根据ID获取编辑属性信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<CategoryAttributeDto> GetAttributeForEdit(Guid Id);
    }
}
