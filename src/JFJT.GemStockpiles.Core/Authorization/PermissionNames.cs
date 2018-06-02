namespace JFJT.GemStockpiles.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// </summary>
    public static class PermissionNames
    {
        // 根节点
        public const string Pages = "Pages";

        #region 系统管理
        public const string Pages_SystemManagement = "Pages.SystemManagement";

        public const string Pages_SystemManagement_Roles = "Pages.SystemManagement.Roles";
        public const string Pages_SystemManagement_Roles_Create = "Pages.SystemManagement.Roles.Create";
        public const string Pages_SystemManagement_Roles_Edit = "Pages.SystemManagement.Roles.Edit";
        public const string Pages_SystemManagement_Roles_Delete = "Pages.SystemManagement.Roles.Delete";
                                  
        public const string Pages_SystemManagement_Users = "Pages.SystemManagement.Users";
        public const string Pages_SystemManagement_Users_Create = "Pages.SystemManagement.Users.Create";
        public const string Pages_SystemManagement_Users_Edit = "Pages.SystemManagement.Users.Edit";
        public const string Pages_SystemManagement_Users_Delete = "Pages.SystemManagement.Users.Delete";

        public const string Pages_SystemManagement_Tenants = "Pages.SystemManagement.Tenants";
        public const string Pages_SystemManagement_Tenants_Create = "Pages.SystemManagement.Tenants.Create";
        public const string Pages_SystemManagement_Tenants_Edit = "Pages.SystemManagement.Tenants.Edit";
        public const string Pages_SystemManagement_Tenants_Delete = "Pages.SystemManagement.Tenants.Delete";
        #endregion

        #region 积分管理
        public const string Pages_PointManagement = "Pages.PointManagement";

        public const string Pages_PointManagement_PointRules = "Pages.PointManagement.PointRules";
        public const string Pages_PointManagement_PointRules_Create = "Pages.PointManagement.PointRules.Create";
        public const string Pages_PointManagement_PointRules_Edit = "Pages.PointManagement.PointRules.Edit";
        public const string Pages_PointManagement_PointRules_Delete = "Pages.PointManagement.PointRules.Delete";
                                  
        public const string Pages_PointManagement_PointRanks = "Pages.PointManagement.PointRanks";
        public const string Pages_PointManagement_PointRanks_Create = "Pages.PointManagement.PointRanks.Create";
        public const string Pages_PointManagement_PointRanks_Edit = "Pages.PointManagement.PointRanks.Edit";
        public const string Pages_PointManagement_PointRanks_Delete = "Pages.PointManagement.PointRanks.Delete";
        #endregion
    }
}
