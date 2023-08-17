using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj_csharp_kiminoyume.Models
{
    [Table("Address")]
    public class Address : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Address1 { get; set; }

        [MaxLength(255)]
        public string? Address2 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(2)]
        public string? State { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(10)]
        public string? Zip { get; set; }


        // navigation properties
        // https://stackoverflow.com/questions/10113244/why-use-icollection-and-not-ienumerable-or-listt-on-many-many-one-many-relatio
        // https://learn.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/dd468057(v=vs.100)

        public int PersonId { get; set; }
        public virtual Person? Person { get; set; } 
    }
}
