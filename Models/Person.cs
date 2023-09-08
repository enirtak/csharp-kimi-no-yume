using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models
{
    [Table("Person")]
    public class Person : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }

        [MaxLength(100)]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? EmailAddress { get; set; }

        [MaxLength(100)]
        public string? LinkedIn { get; set; }

        [MaxLength(100)]
        public string? GitHub { get; set; }

        [MaxLength(200)]
        public string? WebsiteUrl { get; set; }

        [MaxLength(500)]
        public string? AboutMe { get; set; }

        [MaxLength(100)]
        public string? Other { get; set; }

        // navigation properties
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>(); // 1 Person : M Address
        public virtual ICollection<Employer> Employers { get; set; } = new List<Employer>(); // 1 Person : M Employee
        public virtual ICollection<Projects> Projects { get; set; } = new List<Projects>(); // 1 Person : M Projects
        public virtual ICollection<Skills> Skills { get; set; } = new List<Skills>(); // 1 Person : M Skills

    }
}
