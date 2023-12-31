﻿namespace proj_csharp_kiminoyume.Services.Redis
{
    public interface IRedisCacheService
    {
        Task<T?> Get<T>(string key) where T : class;
        Task Set<T>(string key, T value) where T : class;
        Task Clear(string key);
    }
}
