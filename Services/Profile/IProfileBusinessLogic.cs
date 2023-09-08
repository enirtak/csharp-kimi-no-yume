using proj_csharp_kiminoyume.Models;

namespace proj_csharp_kiminoyume.Services.Profile
{
    public interface IProfileBusinessLogic
    {
        Task<List<Person>> GetProfileList(bool getAll = false);
        Task<Person?> GetProfile();
        Task<Person?> GetProfileById(int id);
        Task<Person?> CreateNewProfile(Person request);
        Task<Person?> UpdateProfile(Person request);
    }
}
