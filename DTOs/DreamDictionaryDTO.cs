namespace proj_csharp_kiminoyume.DTOs
{
    public class DreamDictionaryDTO
    {
        public int Id { get; set; }
        public string? DreamName { get; set; }
        public string? DreamDescription { get; set; }

        public int DreamCategoryId { get; set; }
    }
}
