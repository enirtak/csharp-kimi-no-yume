using proj_csharp_kiminoyume.DTOs;

namespace proj_csharp_kiminoyume.Services.DreamDictionary
{
    public interface IDreamDictionaryBusinessLogic
    {
        Task<List<DreamDictionaryDTO>> GetDreamList();
        Task<Models.DreamDictionary?> CreateDream(Models.DreamDictionary request);
        Task<Models.DreamDictionary?> UpdateDream(Models.DreamDictionary request);
        Task DeleteDream(int dreamId);
    }
}
