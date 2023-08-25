using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models
{
    [Table("DreamCategory")]
    public class DreamCategory : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string? CategoryName { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        // Navigation property
        public virtual ICollection<DreamDictionary>? DreamDictionary { get; set; } = new List<DreamDictionary>();
    }
}
