using proj_csharp_kiminoyume.DTOs;

namespace proj_csharp_kiminoyume.Requests
{
    public class DreamRequest
    {
        public class CategoryRequest
        {
            public DreamCategoryDTO Category { get; set; }
        }

        public class DreamIdRequest
        {
            public int Id { get; set; }
        }

        public class DreamDictionaryRequest
        {
            public DreamDictionaryDTO DreamItem { get; set; }
        }
    }
}
