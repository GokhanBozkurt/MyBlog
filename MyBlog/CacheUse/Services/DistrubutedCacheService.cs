using CacheUse.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CacheUse.Services
{
    public interface IDistrubutedCacheService
    {
        Task<T> GetAsync<T>(string key) where T : class;
        Task SetAsync<T>(string key,T value) where T : class;
    }
    public class DistrubutedCacheService : IDistrubutedCacheService
    {
        private readonly ICacheProvider cacheProvider;

        public DistrubutedCacheService(ICacheProvider cacheProvider)
        {
            this.cacheProvider = cacheProvider;
        }
        public async Task<T> GetAsync<T>(string key) where T: class
        {
            return await cacheProvider.GetFromCacheAsync<T>(key);
        }

        public async Task SetAsync<T>(string key, T value) where T: class
        {
            await cacheProvider.SetCacheAsync<T>(key, value);
        }
    }
}
