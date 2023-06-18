using proj_csharp_kiminoyume.DTOs;

namespace proj_csharp_kiminoyume.Responses
{
    public class DreamResponse
    {
        public class CategoryResponse : BaseResponse
        {
            public List<DreamCategoryDTO> Categories { get; set; }
        }
        public class CategoryItemResponse : BaseResponse
        {
            public DreamCategoryDTO Category { get; set; }
        }

        public class DreamListResponse : BaseResponse
        {
            public List<DreamDictionaryDTO> DictionaryList { get; set; }
        }

        public class DreamItemResponse : BaseResponse
        {
            public DreamDictionaryDTO DreamItem { get; set; }
        }
    }
}
