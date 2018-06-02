using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Application.Services.Dto;

namespace JFJT.GemStockpiles.Roles.Dto
{
    [AutoMapFrom(typeof(Permission))]
    public class FlatPermissionDto : EntityDto<long>
    {
        public string ParentName { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }
    }
}
