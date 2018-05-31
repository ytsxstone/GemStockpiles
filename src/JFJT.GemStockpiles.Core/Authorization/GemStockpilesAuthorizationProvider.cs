using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace JFJT.GemStockpiles.Authorization
{
    public class GemStockpilesAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(PermissionNames.Pages) ?? context.CreatePermission(PermissionNames.Pages, L("Pages"));

            #region 系统管理
            var systemManagement = pages.CreateChildPermission(PermissionNames.Pages_SystemManagement, L("SystemManagement"));

            var roles = systemManagement.CreateChildPermission(PermissionNames.Pages_SystemManagement_Roles, L("Roles"));
            roles.CreateChildPermission(PermissionNames.Pages_SystemManagement_Roles_Create, L("CreateRole"));
            roles.CreateChildPermission(PermissionNames.Pages_SystemManagement_Roles_Edit, L("EditRole"));
            roles.CreateChildPermission(PermissionNames.Pages_SystemManagement_Roles_Delete, L("DeleteRole"));

            var users = systemManagement.CreateChildPermission(PermissionNames.Pages_SystemManagement_Users, L("Users"));
            users.CreateChildPermission(PermissionNames.Pages_SystemManagement_Users_Create, L("CreateUser"));
            users.CreateChildPermission(PermissionNames.Pages_SystemManagement_Users_Edit, L("EditUser"));
            users.CreateChildPermission(PermissionNames.Pages_SystemManagement_Users_Delete, L("DeleteUser"));

            //多租户
            pages.CreateChildPermission(PermissionNames.Pages_SystemManagement_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            #endregion

            #region 积分管理
            var pointManagement = pages.CreateChildPermission(PermissionNames.Pages_PointManagement, L("PointManagement"));

            var pointRules = pointManagement.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRules, L("PointRules"));
            pointRules.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRules_Create, L("CreatePointRule"));
            pointRules.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRules_Edit, L("EditPointRule"));
            pointRules.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRules_Delete, L("DeletePointRule"));

            var pointRanks = pointManagement.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRanks, L("PointRanks"));
            pointRanks.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRanks_Create, L("CreatePointRank"));
            pointRanks.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRanks_Edit, L("EditPointRank"));
            pointRanks.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRanks_Delete, L("DeletePointRank"));
            #endregion
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, GemStockpilesConsts.LocalizationSourceName);
        }
    }
}
