namespace proj_csharp_kiminoyume.DTOs
{
    public class ProjectsDTO: BaseDTO
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public int ProjectStatus { get; set; }
        public int PersonId { get; set; }
    }
}
