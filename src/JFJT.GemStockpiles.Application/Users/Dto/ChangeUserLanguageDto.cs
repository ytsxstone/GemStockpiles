using System.ComponentModel.DataAnnotations;

namespace JFJT.GemStockpiles.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}