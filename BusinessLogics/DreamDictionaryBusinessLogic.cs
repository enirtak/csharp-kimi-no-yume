using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Services;
using proj_csharp_kiminoyume.Services.Redis;

namespace proj_csharp_kiminoyume.BusinessLogics
{
    public class DreamDictionaryBusinessLogic : IEntityActionBusinessLogic<DreamDictionary>
    {
        private readonly AppDBContext _context;
        private readonly IRedisCacheService _cache;
        private readonly string _dreamListKey = "dreamList";

        public DreamDictionaryBusinessLogic(AppDBContext context, IRedisCacheService cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<DreamDictionaryDTO>> GetDreamList()
        {
            var dreamList = new List<DreamDictionaryDTO>();
            var isRedisDown = true; // RedisCacheHelper.IsRedisServerDown();

            try
            {
                if (!isRedisDown)
                {
                    var result = await _cache.Get<List<DreamDictionaryDTO>>(_dreamListKey);
                    if (result is not null)
                    {
                        dreamList.AddRange(result);
                        return dreamList;
                    }
                }

                var list = await _context.DreamDictionaries
                    .Where(x => x.IsActive == true)
                    .OrderBy(x => x.DreamName)
                    .ToListAsync();

                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var dreamDTO = ConvertModelToDTO.ConvertDictionaryModelToDTO(item);
                        if (dreamDTO is not null) dreamList.Add(dreamDTO);
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

        public async Task<List<DreamDictionary>> GetList()
        {
            try
            {
                var list = await _context.DreamDictionaries
                    .AsNoTracking()
                    .Where(x => x.IsActive)
                    .OrderBy(x => x.DreamName)
                    .ToListAsync();

                return list;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DreamDictionary?> Create(DreamDictionary request)
        {
            if (request == null) return null;

            try
            {
                var newDream = _context.DreamDictionaries.Add(request);
                await _context.SaveChangesAsync();

                return newDream?.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DreamDictionary?> Update(DreamDictionary request)
        {
            if (request == null) return null;

            try
            {
                var oldEntity = await _context.DreamDictionaries.FindAsync(request.Id);
                if (oldEntity == null) return null;

                _context.Entry(oldEntity).CurrentValues.SetValues(request);
                await _context.SaveChangesAsync();

                return oldEntity;
            }
            catch 
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            if (id == default) return;

            try
            {
                await _context.DreamDictionaries
                        .Where(x => x.Id == id)
                        .ExecuteUpdateAsync(update =>
                            update.SetProperty(dream => dream.IsActive, false));
            }
            catch
            {
                throw;
            }
        }
    }
}
