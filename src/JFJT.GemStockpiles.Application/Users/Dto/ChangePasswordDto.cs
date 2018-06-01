using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Authorization.Users;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Authorization.Users;

namespace JFJT.GemStockpiles.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class ChangePasswordDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string NewPassword { get; set; }
    }
}
