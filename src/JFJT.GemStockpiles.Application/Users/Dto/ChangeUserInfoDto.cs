using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Authorization.Users;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Authorization.Users;

namespace JFJT.GemStockpiles.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class ChangeUserInfoDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }
    }
}
