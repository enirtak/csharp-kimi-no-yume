using proj_csharp_kiminoyume.DTOs;

namespace proj_csharp_kiminoyume.Responses
{
    public class DreamCategoryResponse
    {
        public class CategoryResponse : BaseResponse
        {
            public List<DreamCategoryDTO>? Categories { get; set; }

        }
        public class CategoryItemResponse : BaseResponse
        {
            public DreamCategoryDTO? Category { get; set; }
        }
    }
}
