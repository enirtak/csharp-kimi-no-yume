namespace proj_csharp_kiminoyume.DTOs
{
    public class ProjectsDTO
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public int ProjectStatus { get; set; }
        public bool IsActive { get; set; }
        public int PersonId { get; set; }
    }
}
