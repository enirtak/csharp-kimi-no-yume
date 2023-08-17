using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models
{
    [Table("WorkExperience")]
    public class WorkExperience : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }

        // navigation property
        public int EmployerId { get; set; }
        public virtual Employer? Employer { get; set;}
    }
}
