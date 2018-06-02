using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using JFJT.GemStockpiles.Authorization.Users;

namespace JFJT.GemStockpiles.Authorization.Roles
{
    public class RoleManager : AbpRoleManager<Role, User>
    {
        public RoleManager(
            RoleStore store, 
            IEnumerable<IRoleValidator<Role>> roleValidators, 
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            ILogger<AbpRoleManager<Role, User>> logger,
            IPermissionManager permissionManager, 
            ICacheManager cacheManager, 
            IUnitOfWorkManager unitOfWorkManager,
            IRoleManagementConfig roleManagementConfig)
            : base(
                  store,
                  roleValidators, 
                  keyNormalizer, 
                  errors, logger, 
                  permissionManager,
                  cacheManager, 
                  unitOfWorkManager,
                  roleManagementConfig)
        {
        }

        /// <summary>
        /// ɾ������Ȩ��������
        /// </summary>
        /// <param name="role">The role</param>
        /// <param name="permissions">Permissions</param>
        public override async Task SetGrantedPermissionsAsync(Role role, IEnumerable<Permission> permissions)
        {
            var newPermissions = permissions.ToArray();

            // ɾ������Ȩ��
            await ResetAllPermissionsAsync(role);

            // ����Ȩ��
            foreach (var permission in newPermissions)
            {
                await GrantPermissionAsync(role, permission);
            }
        }
    }
}
