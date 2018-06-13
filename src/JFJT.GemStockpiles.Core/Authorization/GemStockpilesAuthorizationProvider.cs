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
            roles.CreateChildPermission(PermissionNames.Pages_SystemManagement_Roles_View, L("View"));
            roles.CreateChildPermission(PermissionNames.Pages_SystemManagement_Roles_Create, L("Create"));
            roles.CreateChildPermission(PermissionNames.Pages_SystemManagement_Roles_Edit, L("Edit"));
            roles.CreateChildPermission(PermissionNames.Pages_SystemManagement_Roles_Delete, L("Delete"));

            var users = systemManagement.CreateChildPermission(PermissionNames.Pages_SystemManagement_Users, L("Users"));
            users.CreateChildPermission(PermissionNames.Pages_SystemManagement_Users_View, L("View"));
            users.CreateChildPermission(PermissionNames.Pages_SystemManagement_Users_Create, L("Create"));
            users.CreateChildPermission(PermissionNames.Pages_SystemManagement_Users_Edit, L("Edit"));
            users.CreateChildPermission(PermissionNames.Pages_SystemManagement_Users_Delete, L("Delete"));

            var tenants = systemManagement.CreateChildPermission(PermissionNames.Pages_SystemManagement_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(PermissionNames.Pages_SystemManagement_Tenants_View, L("View"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(PermissionNames.Pages_SystemManagement_Tenants_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(PermissionNames.Pages_SystemManagement_Tenants_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(PermissionNames.Pages_SystemManagement_Tenants_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);
            #endregion

            #region 积分管理
            var pointManagement = pages.CreateChildPermission(PermissionNames.Pages_PointManagement, L("PointManagement"));

            var pointRules = pointManagement.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRules, L("PointRules"));
            pointRules.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRules_View, L("View"));
            pointRules.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRules_Create, L("Create"));
            pointRules.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRules_Edit, L("Edit"));
            pointRules.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRules_Delete, L("Delete"));

            var pointRanks = pointManagement.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRanks, L("PointRanks"));
            pointRanks.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRanks_View, L("View"));
            pointRanks.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRanks_Create, L("Create"));
            pointRanks.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRanks_Edit, L("Edit"));
            pointRanks.CreateChildPermission(PermissionNames.Pages_PointManagement_PointRanks_Delete, L("Delete"));
            #endregion

            #region 商品管理
            var ProductManagement = pages.CreateChildPermission(PermissionNames.Pages_ProductManagement, L("ProductManagement"));

            var product = ProductManagement.CreateChildPermission(PermissionNames.Pages_ProductManagement_Products, L("Products"));
            product.CreateChildPermission(PermissionNames.Pages_ProductManagement_Products_View, L("View"));
            product.CreateChildPermission(PermissionNames.Pages_ProductManagement_Products_Create, L("Create"));
            product.CreateChildPermission(PermissionNames.Pages_ProductManagement_Products_Edit, L("Edit"));
            product.CreateChildPermission(PermissionNames.Pages_ProductManagement_Products_Delete, L("Delete"));

            var settings = ProductManagement.CreateChildPermission(PermissionNames.Pages_ProductManagement_Categorys, L("ProductCategorys"));
            settings.CreateChildPermission(PermissionNames.Pages_ProductManagement_Categorys_View, L("View"));
            settings.CreateChildPermission(PermissionNames.Pages_ProductManagement_Categorys_Create, L("Create"));
            settings.CreateChildPermission(PermissionNames.Pages_ProductManagement_Categorys_Edit, L("Edit"));
            settings.CreateChildPermission(PermissionNames.Pages_ProductManagement_Categorys_Delete, L("Delete"));

            var analysis = ProductManagement.CreateChildPermission(PermissionNames.Pages_ProductManagement_CategoryAttributes, L("CategoryAttributes"));
            analysis.CreateChildPermission(PermissionNames.Pages_ProductManagement_CategoryAttributes_View, L("View"));
            analysis.CreateChildPermission(PermissionNames.Pages_ProductManagement_CategoryAttributes_Create, L("Create"));
            analysis.CreateChildPermission(PermissionNames.Pages_ProductManagement_CategoryAttributes_Edit, L("Edit"));
            analysis.CreateChildPermission(PermissionNames.Pages_ProductManagement_CategoryAttributes_Delete, L("Delete"));
            #endregion

            #region 订单管理
            var OrderManagement = pages.CreateChildPermission(PermissionNames.Pages_OrderManagement, L("OrderManagement"));

            var orders = OrderManagement.CreateChildPermission(PermissionNames.Pages_OrderManagement_Orders, L("Orders"));
            orders.CreateChildPermission(PermissionNames.Pages_OrderManagement_Orders_View, L("View"));
            orders.CreateChildPermission(PermissionNames.Pages_OrderManagement_Orders_Create, L("Create"));
            orders.CreateChildPermission(PermissionNames.Pages_OrderManagement_Orders_Edit, L("Edit"));
            orders.CreateChildPermission(PermissionNames.Pages_OrderManagement_Orders_Delete, L("Delete"));

            var comment = OrderManagement.CreateChildPermission(PermissionNames.Pages_OrderManagement_Comments, L("OrderComments"));
            comment.CreateChildPermission(PermissionNames.Pages_OrderManagement_Comments_View, L("View"));
            comment.CreateChildPermission(PermissionNames.Pages_OrderManagement_Comments_Create, L("Create"));
            comment.CreateChildPermission(PermissionNames.Pages_OrderManagement_Comments_Edit, L("Edit"));
            comment.CreateChildPermission(PermissionNames.Pages_OrderManagement_Comments_Delete, L("Delete"));
            #endregion
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, GemStockpilesConsts.LocalizationSourceName);
        }
    }
}
