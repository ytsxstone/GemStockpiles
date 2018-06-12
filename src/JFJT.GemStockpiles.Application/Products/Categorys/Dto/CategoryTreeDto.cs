using System;
using System.Collections.Generic;

namespace JFJT.GemStockpiles.Products.Categorys.Dto
{
    public class CategoryTreeDto
    {
        public Guid value { get; set; }

        public string title { get; set; }

        public int level { get; set; }

        public bool expand { get; set; } = true;

        public List<CategoryTreeDto> children { get; set; } = new List<CategoryTreeDto>();
    }
}
