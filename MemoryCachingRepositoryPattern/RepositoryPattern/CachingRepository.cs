using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.MemoryCaching.RepositoryPattern
{
    public class CachingRepository<T> : ICachingRepository<T> where T : class
    {
        private readonly IMemoryCache _cache;

        public CachingRepository(IMemoryCache cache)
        {
            _cache = cache;
        }
        #region basic methods
        public async Task<T> GetCachedAsync(object key, Func<Task<T>> retrieveData)
        {
            if (_cache.TryGetValue(key, out T cachedData))
            {
                return cachedData;
            }

            var data = await retrieveData();
            _cache.Set(key, data, TimeSpan.FromMinutes(5)); // Adjust expiration time as needed
            return data;
        }

        public async Task<IEnumerable<T>> GetAllCachedAsync(string key, Func<Task<IEnumerable<T>>> retrieveData)
        {
            if (_cache.TryGetValue(key, out IEnumerable<T> cachedData))
            {
                return cachedData;
            }

            var data = await retrieveData();
            _cache.Set(key, data, TimeSpan.FromMinutes(5)); // Adjust expiration time as needed
            return data;
        }

        public void RemoveFromCache(object key)
        {
            _cache.Remove(key);
        }

        public void SetCache(object key, T item, TimeSpan? expirationTime = null)
        {
            _cache.Set(key, item, expirationTime ?? TimeSpan.FromMinutes(5));
        }
        #endregion

        #region Repository Methods

        #endregion
    }
}
