namespace proj_csharp_kiminoyume.DTOs
{
    public class WorkExperienceDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        public int EmployerId { get; set; }
        public bool IsActive { get; set; }
    }
}
