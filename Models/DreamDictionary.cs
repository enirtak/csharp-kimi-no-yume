using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models
{
    [Table("DreamDictionary")]
    public class DreamDictionary: BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? DreamName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? DreamDescription { get; set; }

        // FK to DreamCategory
        // DreamId = DreamDictionary.Id
        public int DreamCategoryId { get; set; }

        // Navigation property
        public virtual DreamCategory? DreamCategory { get; set; }
    }
}
