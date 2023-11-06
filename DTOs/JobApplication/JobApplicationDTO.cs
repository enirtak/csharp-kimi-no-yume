using proj_csharp_kiminoyume.Models.JobApplication;

namespace proj_csharp_kiminoyume.DTOs.JobApplication
{
    public class JobApplicationDTO : BaseDTO
    {
        public string? JobTitle { get; set; }
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
        public List<JobAppCustomFieldDTO>? CustomFields { get; set; } 
    }
}
