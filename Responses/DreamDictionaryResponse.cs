using proj_csharp_kiminoyume.DTOs;

namespace proj_csharp_kiminoyume.Responses
{
    public class DreamDictionaryResponse
    {
        public class DictionaryResponse : BaseResponse
        {
            public List<DreamDictionaryDTO>? DictionaryList { get; set; }
        }

        public class DictionarytemResponse : BaseResponse
        {
            public DreamDictionaryDTO? DreamItem { get; set; }
        }
    }
}
