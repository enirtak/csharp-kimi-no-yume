using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Services;

namespace proj_csharp_kiminoyume.BusinessLogics
{
    public class DreamBusinessLogic
    {
        private AppDBContext _context;
        private RedisCacheService _cache;
        private string _dreamListKey = "dreamList";
        private bool _isRedisDown = false;

        public DreamBusinessLogic(AppDBContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = new RedisCacheService(cache);
            _isRedisDown = new Helpers.RedisCacheHelper().IsRedisServerDown();
        }

        public async Task<List<DreamDictionaryDTO>> GetDreamList()
        {
            var dreamList = new List<DreamDictionaryDTO>();

            try
            {
                if (!_isRedisDown)
                {
                    var result = await _cache.Get<List<DreamDictionaryDTO>>(_dreamListKey);
                    if (result != null)
                    {
                        dreamList.AddRange(result);
                        return dreamList;
                    }
                }

                var list = await _context.DreamDictionaries.OrderBy(x => x.Id).ToListAsync();
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var dreamDTO = new Helpers.ConvertModelToDTO().ConvertDictionaryModelToDTO(item);
                        if (dreamDTO != null) dreamList.Add(dreamDTO);
                    }
                }

                if (!_isRedisDown) await _cache.Set(_dreamListKey, list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dreamList;
        }

        public async Task<DreamDictionaryDTO> GetDreamById(int id)
        {
            var dreamItem = new DreamDictionaryDTO();

            try
            {
                if (id > 0)
                {
                    var item = await _context.DreamDictionaries.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (item != null)
                    {
                        dreamItem = new Helpers.ConvertModelToDTO().ConvertDictionaryModelToDTO(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dreamItem;
        }

        public async Task CreateDream(DreamDictionary request)
        {
            try
            {
                if (request != null)
                {
                    request.CreatedDate = DateTime.Now;
                    request.CreatedBy = "katorin";
                    request.LastUpdatedDate = DateTime.Now;
                    request.LastUpdatedBy = "katorin";

                    _context.DreamDictionaries.Add(request);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateDream(DreamDictionary request)
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

                        _context.DreamDictionaries.Update(existingDef);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                        var category = new Helpers.ConvertModelToDTO().ConvertCategoryModelToDTO(item);
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

        public async Task CreateCategory(DreamCategory category)
        {
            try
            {
                if (category != null)
                {
                    _context.DreamCategories.Add(category);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCategoryById(DreamCategory categories)
        {
            try
            {
                if (categories != null)
                {
                    var category = _context.DreamCategories.Where(x => x.Id == categories.Id).FirstOrDefault();
                    if (category != null)
                    {
                        // replace with new values
                        category.CategoryName = categories.CategoryName;

                        _context.DreamCategories.Update(category);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
