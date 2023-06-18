using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace proj_csharp_kiminoyume.Services
{
    public class RedisCacheService
    {
        private static IDistributedCache _cache;
        private DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddDays(30));

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> Get<T>(string key) where T: class
        {
            var cachedValue = await _cache.GetStringAsync(key);
            return cachedValue == null ? null : JsonSerializer.Deserialize<T>(cachedValue);
        }

        public async Task Set<T>(string key, T value) where T : class
        {
            var data = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, data, options);
        }

        public async Task Clear(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
