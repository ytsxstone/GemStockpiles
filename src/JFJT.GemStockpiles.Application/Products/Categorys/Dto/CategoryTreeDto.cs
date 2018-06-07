using System;
using System.Collections.Generic;
using System.Text;

namespace JFJT.GemStockpiles.Products.Category.Dto
{
    public class CategoryTreeDto
    {
        public string title { get; set; }

        //public string name { get; set; }

        public int level { get; set; }

        public bool expand { get; set; } = true;

        public List<CategoryTreeDto> children { get; set; } = new List<CategoryTreeDto>();
    }
}
