using proj_csharp_kiminoyume.DTOs;

namespace proj_csharp_kiminoyume.Services.DreamCategory
{
    public interface IDreamCategoryBusinessLogic
    {
        Task<List<DreamCategoryDTO>> GetCategoriesList();
        Task<Models.DreamCategory?> CreateCategory(Models.DreamCategory category);
        Task<Models.DreamCategory?> UpdateCategory(Models.DreamCategory category);
        Task DeleteCategory(int id);
    }
}
