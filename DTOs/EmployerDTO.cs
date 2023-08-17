namespace proj_csharp_kiminoyume.DTOs
{
    public class EmployerDTO
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Zip { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }

        public int PersonId { get; set; }
        public List<WorkExperienceDTO> WorkExps { get; set; }
    }
}
