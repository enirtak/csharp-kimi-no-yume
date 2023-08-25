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
                // @KAT TODO: Add NG validation for model required fields
                var list = await _context.DreamCategories.OrderBy(x => x.CategoryName).ToListAsync();
                if (list!= null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var category = ConvertModelToDTO.ConvertCategoryModelToDTO(item);
                        if (category != null) categoriesList.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return categoriesList;
        }

        public async Task<DreamCategory?> CreateCategory(DreamCategory category)
        {
            try
            {
                if (category != null)
                {
                    var newEntity = _context.DreamCategories.Add(category);
                    await _context.SaveChangesAsync();
                    return newEntity?.Entity;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }

        public async Task<DreamCategory?> UpdateCategory(DreamCategory categories)
        {
            try
            {
                if (categories != null)
                {
                    var oldCategory = _context.DreamCategories.Where(x => x.Id == categories.Id).FirstOrDefault();
                    if (oldCategory != null)
                    {
                        oldCategory.CategoryName = categories.CategoryName;
                        oldCategory.Description = categories.Description;

                        var newEntity = _context.DreamCategories.Update(oldCategory);
                        await _context.SaveChangesAsync();
                        return newEntity?.Entity;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }

        public async Task DeleteCategory(int categoryId)
        {
            try
            {
                if (categoryId != default)
                {
                    var dreamTheme = _context.DreamCategories.Where(x => x.Id == categoryId).FirstOrDefault();
                    if (dreamTheme != null)
                    {
                        dreamTheme.IsActive = false;

                        _context.DreamCategories.Update(dreamTheme);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
