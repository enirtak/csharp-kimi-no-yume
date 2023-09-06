namespace proj_csharp_kiminoyume.DTOs
{
    public class DreamDictionaryDTO : BaseDTO
    {
        public string? DreamName { get; set; }
        public string? DreamDescription { get; set; }

        public int DreamCategoryId { get; set; }
    }
}
