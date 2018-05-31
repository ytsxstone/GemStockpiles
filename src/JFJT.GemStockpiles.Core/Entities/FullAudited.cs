using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace JFJT.GemStockpiles.Entities
{
    #region ABP基类说明
    /*
     IAudited : ICreationAudited, IModificationAudited 自动记录创建修改人时间信息 可直接继承 IAudited
     IDeletionAudited : ISoftDelete  逻辑删除,继承IDeletionAudited ABP自动会过滤删除掉的数据
     IFullAudited : IAudited, IDeletionAudited 继承  IFullAudited  可包含修改创建 删除
         */
    #endregion

    public class FullAudited : FullAudited<int>
    {

    }

    public class FullAudited<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited, IModificationAudited, IDeletionAudited
    {
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
