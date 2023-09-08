using proj_csharp_kiminoyume.DTOs;

namespace proj_csharp_kiminoyume.Responses
{
    public class ProfileResponse : BaseResponse
    {
        public PersonDTO? Person { get; set; }
    }

    public class ProfileListResponse : BaseResponse
    {
        public List<PersonDTO?> Persons { get; set; } = new List<PersonDTO?>();
    }
}
