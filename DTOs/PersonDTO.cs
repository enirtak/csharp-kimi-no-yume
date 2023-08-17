using proj_csharp_kiminoyume.Models;

namespace proj_csharp_kiminoyume.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? AboutMe { get; set; }
        public string? Other { get; set; }
        public bool IsActive { get; set; } = true;

        //public int AddressId { get; set; }
        //public int EmployerId { get; set; }
        //public int SkillsId { get; set; }
        //public int ProjectsId { get; set; }

        public List<AddressDTO>? Addresses { get; set; }
        public List<EmployerDTO>? Employers { get; set; }
        public List<SkillsDTO>? Skills { get; set; }
        public List<ProjectsDTO>? Projects { get; set; }
    }
}
