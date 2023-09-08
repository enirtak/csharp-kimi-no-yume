using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models
{
    [Table("Skills")]
    public class Skills : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public string? SkillName { get; set; }
        public int SkillType { get; set; }

        // navigation property
        public int PersonId { get; set; }
        public virtual Person? Person { get; set; }
    }
}
