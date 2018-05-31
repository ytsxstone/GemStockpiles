using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace JFJT.GemStockpiles.Entities
{
    public class ModifyDeleteAudited : ModifyDeleteAudited<int>
    {

    }

    public class ModifyDeleteAudited<TPrimaryKey> : Entity<TPrimaryKey>, IModificationAudited, IDeletionAudited
    {
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
