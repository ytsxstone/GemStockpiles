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
        public const string Pages_SystemManagement_Roles_View = "Pages.SystemManagement.Roles.View";
        public const string Pages_SystemManagement_Roles_Create = "Pages.SystemManagement.Roles.Create";
        public const string Pages_SystemManagement_Roles_Edit = "Pages.SystemManagement.Roles.Edit";
        public const string Pages_SystemManagement_Roles_Delete = "Pages.SystemManagement.Roles.Delete";
                                  
        public const string Pages_SystemManagement_Users = "Pages.SystemManagement.Users";
        public const string Pages_SystemManagement_Users_View = "Pages.SystemManagement.Users.View";
        public const string Pages_SystemManagement_Users_Create = "Pages.SystemManagement.Users.Create";
        public const string Pages_SystemManagement_Users_Edit = "Pages.SystemManagement.Users.Edit";
        public const string Pages_SystemManagement_Users_Delete = "Pages.SystemManagement.Users.Delete";

        public const string Pages_SystemManagement_Tenants = "Pages.SystemManagement.Tenants";
        public const string Pages_SystemManagement_Tenants_View = "Pages.SystemManagement.Tenants.View";
        public const string Pages_SystemManagement_Tenants_Create = "Pages.SystemManagement.Tenants.Create";
        public const string Pages_SystemManagement_Tenants_Edit = "Pages.SystemManagement.Tenants.Edit";
        public const string Pages_SystemManagement_Tenants_Delete = "Pages.SystemManagement.Tenants.Delete";
        #endregion

        #region 积分管理
        public const string Pages_PointManagement = "Pages.PointManagement";

        public const string Pages_PointManagement_PointRules = "Pages.PointManagement.PointRules";
        public const string Pages_PointManagement_PointRules_View = "Pages.PointManagement.PointRules.View";
        public const string Pages_PointManagement_PointRules_Create = "Pages.PointManagement.PointRules.Create";
        public const string Pages_PointManagement_PointRules_Edit = "Pages.PointManagement.PointRules.Edit";
        public const string Pages_PointManagement_PointRules_Delete = "Pages.PointManagement.PointRules.Delete";
                                  
        public const string Pages_PointManagement_PointRanks = "Pages.PointManagement.PointRanks";
        public const string Pages_PointManagement_PointRanks_View = "Pages.PointManagement.PointRanks.View";
        public const string Pages_PointManagement_PointRanks_Create = "Pages.PointManagement.PointRanks.Create";
        public const string Pages_PointManagement_PointRanks_Edit = "Pages.PointManagement.PointRanks.Edit";
        public const string Pages_PointManagement_PointRanks_Delete = "Pages.PointManagement.PointRanks.Delete";
        #endregion

        #region 商品管理
        public const string Pages_ProductManagement = "Pages.ProductManagement";

        public const string Pages_ProductManagement_Products = "Pages.ProductManagement.Products";
        public const string Pages_ProductManagement_Products_View = "Pages.ProductManagement.Products.View";
        public const string Pages_ProductManagement_Products_Create = "Pages.ProductManagement.Products.Create ";
        public const string Pages_ProductManagement_Products_Edit = "Pages.ProductManagement.Products.Edit";
        public const string Pages_ProductManagement_Products_Delete = "Pages.ProductManagement.Products.Delete";

        public const string Pages_ProductManagement_Analysis = "Pages.ProductManagement.Analysis";
        public const string Pages_ProductManagement_Analysis_View = "Pages.ProductManagement.Analysis.View";
        public const string Pages_ProductManagement_Analysis_Create = "Pages.ProductManagement.Analysis.Create";
        public const string Pages_ProductManagement_Analysis_Edit = "Pages.ProductManagement.Analysis.Edit";
        public const string Pages_ProductManagement_Analysis_Delete = "Pages.ProductManagement.Analysis.Delete";

        public const string Pages_ProductManagement_Categorys = "Pages.ProductManagement.Categorys";
        public const string Pages_ProductManagement_Categorys_View = "Pages.ProductManagement.Categorys.View";
        public const string Pages_ProductManagement_Categorys_Create = "Pages.ProductManagement.Categorys.Create";
        public const string Pages_ProductManagement_Categorys_Edit = "Pages.ProductManagement.Categorys.Edit";
        public const string Pages_ProductManagement_Categorys_Delete = "Pages.ProductManagement.Categorys.Delete";
        #endregion


        #region 订单管理
        public const string Pages_OrderManagement = "Pages.OrderManagement";

        public const string Pages_OrderManagement_Comments = "Pages.OrderManagement.Comments";
        public const string Pages_OrderManagement_Comments_View = "Pages.OrderManagement.Comments.View";
        public const string Pages_OrderManagement_Comments_Create = "Pages.OrderManagement.Comments.Create ";
        public const string Pages_OrderManagement_Comments_Edit = "Pages.OrderManagement.Comments.Edit";
        public const string Pages_OrderManagement_Comments_Delete = "Pages.OrderManagement.Comments.Delete";

        public const string Pages_OrderManagement_Orders = "Pages.OrderManagement.Orders";
        public const string Pages_OrderManagement_Orders_View = "Pages.OrderManagement.Orders.View";
        public const string Pages_OrderManagement_Orders_Create = "Pages.OrderManagement.Orders.Create ";
        public const string Pages_OrderManagement_Orders_Edit = "Pages.OrderManagement.Orders.Edit";
        public const string Pages_OrderManagement_Orders_Delete = "Pages.OrderManagement.Orders.Delete";
        #endregion
    }
}
