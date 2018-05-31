using System;
using JFJT.GemStockpiles.Enums;
using JFJT.GemStockpiles.Entities;

namespace JFJT.GemStockpiles.Models.Customers
{
    /// <summary>
    /// 客户表
    /// </summary>
    public class Customer : ModifyDeleteAudited
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 注册手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public UserRoleTypeEnum RoleType { get; set; } = UserRoleTypeEnum.Person;

        /// <summary>
        /// 账号状态
        /// </summary>
        public UserStateEnum State { get; set; } = UserStateEnum.Submited;

        /// <summary>
        /// 审核人登录帐号
        /// </summary>
        public string AuditedAccount { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditedTime { get; set; }

        /// <summary>
        /// 驳回原因
        /// </summary>
        public string RejectedRemark { get; set; }

        /// <summary>
        /// 注册IP
        /// </summary>
        public string RegisterIP { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegisterTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后访问IP
        /// </summary>
        public string LastVisitIP { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime? LastVisitTime { get; set; } = DateTime.Now;
    }
}
