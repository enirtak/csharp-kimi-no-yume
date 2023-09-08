using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models
{
    [Table("Projects")]
    public class Projects : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public int ProjectStatus { get; set; }

        // navigation property
        public int PersonId { get; set; }
        public virtual Person? Person { get; set; }
    }
}
