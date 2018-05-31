using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace JFJT.GemStockpiles.Entities
{
    public class CreateModifyAudited : CreateModifyAudited<int>
    {

    }

    public class CreateModifyAudited<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited, IModificationAudited
    {
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
