using System;
using System.Collections.Generic;

namespace JFJT.GemStockpiles.Products.Categorys.Dto
{
    public class CategoryCascaderDto
    {
        public Guid value { get; set; }

        public string label { get; set; }

        public List<CategoryCascaderDto> children { get; set; } = new List<CategoryCascaderDto>();
    }
}
