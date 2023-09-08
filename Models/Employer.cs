using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models
{
    [Table("Employer")]
    public class Employer : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string? CompanyName { get; set; }

        [MaxLength(100)]
        public string? Position { get; set; }

        public decimal? Salary { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [MaxLength(255)]
        public string? Address1 { get; set; }

        [MaxLength(255)]
        public string? Address2 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(5)]
        public string? State { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(10)]
        public string? Zip { get; set; }

        // navigation propertiesss : M Employee
        public int PersonId { get; set; }
        public virtual Person? Person { get; set; } // 1 Person: M Employees
        public virtual ICollection<WorkExperience> WorkExperience { get; set; } = new List<WorkExperience>(); // 1 Employee : M Work Experience
    }
}
