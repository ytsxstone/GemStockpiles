using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Application.Services.Dto;

namespace JFJT.GemStockpiles.Roles.Dto
{
    [AutoMapFrom(typeof(Permission))]
    public class PermissionDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }
    }
}
