using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models.JobApplication
{
    [Table("JobApplication")]
    public class JobApplication : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? JobTitle { get; set; }

        [Required]
        public string? CompanyName { get; set; }

        public string? CompanyWebsite { get; set; }

        public string? WorkLocation { get; set; }

        public string? WorkArrangement { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public string? ApplicationStatus { get; set; }

        public string? ApplicationEmail { get; set; }

        public string? ApplicationSource { get; set; }

        public string? JobType { get; set; }

        public decimal? Salary { get; set; }

        // Navigational Property
        public virtual ICollection<JobAppCustomField>? CustomFields { get; set; } = new List<JobAppCustomField>();
    }
}
