using System.Collections.Generic;

namespace JFJT.GemStockpiles.Roles.Dto
{
    public class PermissionTreeDto
    {
        public string title { get; set; }

        public string name { get; set; }

        public int level { get; set; }

        public bool expand { get; set; } = true;

        public bool selected { get; set; } = false;

        public List<PermissionTreeDto> children { get; set; } = new List<PermissionTreeDto>();
    }
}
