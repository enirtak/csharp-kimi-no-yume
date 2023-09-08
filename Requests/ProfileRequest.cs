using proj_csharp_kiminoyume.DTOs;

namespace proj_csharp_kiminoyume.Requests
{
    public class ProfileRequest
    {
        public PersonDTO? Person { get; set; }
    }

    public class ProfileListRequest
    {
        public bool IsGetAll { get; set; } = false;
    }
}
