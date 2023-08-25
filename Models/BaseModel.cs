using System.ComponentModel;

namespace proj_csharp_kiminoyume.Models
{
    public class BaseModel
    {
        [DefaultValue(true)]
        public bool? IsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public string? CreatedBy { get; set; } = Environment.UserName;

        public DateTime? LastUpdatedDate { get; set; } = DateTime.Now;

        public string? LastUpdatedBy { get; set; } = Environment.UserName;
    }
}
