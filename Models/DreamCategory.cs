using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models
{
    [Table("DreamCategory")]
    public class DreamCategory
    {
        public DreamCategory()
        {
            DreamDictionary = new List<DreamDictionary>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? CategoryName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Description { get; set; }

        // Navigation property
        public ICollection<DreamDictionary> DreamDictionary { get; set; }
    }
}
