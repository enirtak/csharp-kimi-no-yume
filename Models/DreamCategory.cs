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

        public string? CategoryName { get; set; }

        // Navigation property
        public IList<DreamDictionary> DreamDictionary { get; set; }
    }
}
