using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CacheUse.Infrastructure
{
    public interface ICacheProvider
    {
        Task<T> GetFromCacheAsync<T>(string key) where T:class;
        Task SetCacheAsync<T>(string key, T value) where T : class;

    }
    public class CacheProvider : ICacheProvider
    {
        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromSeconds(30)
        };

        private readonly IDistributedCache distrubutedCache;
        public CacheProvider(IDistributedCache distrubutedCache)
        {
            this.distrubutedCache = distrubutedCache;
        }

        public async Task<T> GetFromCacheAsync<T>(string key) where T : class
        {
            var cachedValue = await distrubutedCache.GetStringAsync(key);
            return cachedValue == null ? null : JsonSerializer.Deserialize<T>(cachedValue);
        }

        public async Task SetCacheAsync<T>(string key, T value) where T : class
        {
            var cacheValue = JsonSerializer.Serialize(value);
            await distrubutedCache.SetStringAsync(key, cacheValue, options);
        }
    }
}
