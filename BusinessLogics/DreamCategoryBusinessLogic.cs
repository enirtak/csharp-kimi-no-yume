using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Services.DreamCategory;

namespace proj_csharp_kiminoyume.BusinessLogics
{
    public class DreamCategoryBusinessLogic : IDreamCategoryBusinessLogic
    {
        private readonly AppDBContext _context;

        public DreamCategoryBusinessLogic(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<DreamCategoryDTO>> GetCategoriesList()
        {
            var categoriesList = new List<DreamCategoryDTO>();

            try
            {
                var list = await _context.DreamCategories
                    .Where(x => x.IsActive == true)
                    .OrderBy(x => x.CategoryName)
                    .ToListAsync();

                if (list is not null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var category = ConvertModelToDTO.ConvertCategoryModelToDTO(item);
                        if (category is not null) categoriesList.Add(category);
                    }
                }
            }
            catch
            {
                throw;
            }

            return categoriesList;
        }

        public async Task<DreamCategory?> CreateCategory(DreamCategory category)
        {
            try
            {
                if (category is not null)
                {
                    var newEntity = _context.DreamCategories.Add(category);
                    await _context.SaveChangesAsync();
                    return newEntity?.Entity;
                }
            }
            catch
            {
                throw;
            }

            return null;
        }

        public async Task<DreamCategory?> UpdateCategory(DreamCategory category)
        {
            DreamCategory? dreamCategory = null;

            try
            {
                if (category is not null && category.Id != default)
                {
                    dreamCategory = await _context.DreamCategories.FindAsync(category.Id);
                    if (dreamCategory is not null)
                    {
                        // https://stackoverflow.com/a/30824229 https://stackoverflow.com/a/60919670
                        // context.Entry(entity).State = EntityState.Modified | DbSet.Update -> this will update all the fields on the entity
                        // DbSet.Attach(entity) -> will only update dirty fields

                        _context.Entry(dreamCategory).CurrentValues.SetValues(category);
                        await _context.SaveChangesAsync();

                        return dreamCategory;
                    }
                }
            }
            catch
            {
                throw;
            }

            return dreamCategory;
        }

        public async Task DeleteCategory(int categoryId)
        {
            try
            {
                if (categoryId != default)
                {
                    await _context.DreamCategories
                            .Where(x => x.Id == categoryId)
                            .ExecuteUpdateAsync(update =>
                                update.SetProperty(dream => dream.IsActive, false));
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
