using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Interfaces;
using proj_csharp_kiminoyume.Models;

namespace proj_csharp_kiminoyume.BusinessLogics
{
    public class DreamBusinessLogic
    {
        private readonly AppDBContext _context;
        private readonly IRedisCacheService _cache;
        private readonly string _dreamListKey = "dreamList";

        public DreamBusinessLogic(AppDBContext context, IRedisCacheService cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<DreamDictionaryDTO>> GetDreamList()
        {
            var dreamList = new List<DreamDictionaryDTO>();
            var isRedisDown = RedisCacheHelper.IsRedisServerDown();

            try
            {
                if (!isRedisDown)
                {
                    var result = await _cache.Get<List<DreamDictionaryDTO>>(_dreamListKey);
                    if (result != null)
                    {
                        dreamList.AddRange(result);
                        return dreamList;
                    }
                }

                var list = await _context.DreamDictionaries.OrderBy(x => x.DreamName).ToListAsync();
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var dreamDTO = ConvertModelToDTO.ConvertDictionaryModelToDTO(item);
                        if (dreamDTO != null) dreamList.Add(dreamDTO);
                    }
                }

                if (!isRedisDown) await _cache.Set(_dreamListKey, list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dreamList;
        }

        public async Task<DreamDictionary?> CreateDream(DreamDictionary request)
        {
            try
            {
                if (request != null)
                {
                    request.CreatedDate = DateTime.Now;
                    request.CreatedBy = "katorin";
                    request.LastUpdatedDate = DateTime.Now;
                    request.LastUpdatedBy = "katorin";

                    var newDream = _context.DreamDictionaries.Add(request);
                    await _context.SaveChangesAsync();
                    return newDream?.Entity;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }

        public async Task<DreamDictionary?> UpdateDream(DreamDictionary request)
        {
            try
            {
                if (request != null)
                {
                    var existingDef = _context.DreamDictionaries.AsNoTracking().Where(x => x.Id == request.Id).FirstOrDefault();
                    if (existingDef != null)
                    {
                        request.CreatedDate = existingDef.CreatedDate;
                        request.CreatedBy = existingDef.CreatedBy;
                        request.LastUpdatedBy = existingDef.LastUpdatedBy;
                        request.LastUpdatedDate = DateTime.Now;

                        existingDef = request;

                        var result = _context.DreamDictionaries.Update(existingDef);
                        await _context.SaveChangesAsync();
                        return result?.Entity;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }

        public async Task DeleteDream(DreamDictionary request)
        {
            try
            {
                if (request != null)
                {
                    var item = _context.DreamDictionaries.Where(x => x.Id == request.Id).FirstOrDefault();
                    if (item != null)
                    {
                        _context.DreamDictionaries.Remove(item);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DreamCategoryDTO>> GetCategoriesList()
        {
            var categoriesList = new List<DreamCategoryDTO>();

            try
            {
                var list = await _context.DreamCategories.OrderBy(x => x.CategoryName).ToListAsync();
                if (list.Count > 0)
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

        public async Task DeleteCategory(DreamCategory categories)
        {
            try
            {
                if (categories != null)
                {
                    var dreamTheme = _context.DreamCategories.Where(x => x.Id == categories.Id).FirstOrDefault();
                    if (dreamTheme != null)
                    {
                        _context.DreamCategories.Remove(dreamTheme);
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
