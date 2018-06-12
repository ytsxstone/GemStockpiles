using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Authorization.Users;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Authorization.Users;

namespace JFJT.GemStockpiles.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class CreateUserDto : IShouldNormalize
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string[] RoleNames { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        [Required]
        public virtual UserTypeEnum UserType { get; set; } = UserTypeEnum.Administrator;

        public void Normalize()
        {
            if (RoleNames == null)
            {
                RoleNames = new string[0];
            }
        }
    }
}
