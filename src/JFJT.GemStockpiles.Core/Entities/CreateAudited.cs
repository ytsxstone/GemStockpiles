using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace JFJT.GemStockpiles.Entities
{
    public class CreateAudited : CreateAudited<int>
    {

    }

    public class CreateAudited<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
    {
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
