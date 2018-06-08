using System;
using System.Collections.Generic;
using System.Text;

namespace JFJT.GemStockpiles.Products.Category.Dto
{
    public class CategoryCascaderDto
    {
        public Guid value { get; set; }
        public string label { get; set; }

        public List<CategoryCascaderDto> children { get; set; } = new List<CategoryCascaderDto>();
    }
}
