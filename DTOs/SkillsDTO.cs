namespace proj_csharp_kiminoyume.DTOs
{
    public class SkillsDTO
    {
        public int Id { get; set; }
        public string? SkillName { get; set; }
        public int SkillType { get; set; }
        public int PersonId { get; set; }
        public bool IsActive { get; set; }
    }
}
