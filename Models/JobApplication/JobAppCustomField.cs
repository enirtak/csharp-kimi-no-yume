using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models.JobApplication
{
    [Table("JobApplicationCustomField")]
    public class JobAppCustomField
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? CustomFieldName { get; set; }

        [Required]
        public string? FieldNameValue { get; set; }
        public bool IsActive { get; set; }

        // Navigational Property
        public int JobApplicationId { get; set; }
        public virtual JobApplication? JobApplication { get; set; }
    }
}
