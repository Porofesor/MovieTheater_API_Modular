using DataAccess.EFCore.Extends;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace DataAccess.MemoryCaching.RepositoryPattern
{
    // TODO not sure about ICachingRepository<T,TKey> <= TKey?
    public class CachingRepository<T, TKey, CustomDbContext> : ICachingRepository<T,TKey>
        where T : class, IEntity<TKey>
        where CustomDbContext : DbContext
        where TKey : struct
    {
        private readonly IMemoryCache _cache;
        protected readonly CustomDbContext _context;
        private readonly DbSet<T> _dbSet;
        private const int SET_SLIDING_EXPIRATION_MINUTES = 5;
        private const int SET_ABSOLUTE_EXPIRATION_MINUTES = 5;
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;

        public CachingRepository(IMemoryCache cache, CustomDbContext context)
        {
            _cache = cache;
            _context = context;
            _dbSet = _context.Set<T>();
            _memoryCacheEntryOptions = memoryCacheEntryOptions();
        }

        #region basic methods for caching
        // Unnessesary optimalization made by author, made for fun and tests
        /*
        private async Task CachElements(string cachekey, Task<IEnumerable<T>> entities, MemoryCacheEntryOptions memoryCacheEntryOptions)
        {
            _cache.Set(cachekey, await entities, memoryCacheEntryOptions);
        }
        private async Task<T> GetCachedAsync(object key, Func<Task<T>> retrieveData)
        {
            if (_cache.TryGetValue(key, out T cachedData))
            {
                return cachedData;
            }

            var data = await retrieveData();
            _cache.Set(key, data, TimeSpan.FromMinutes(SET_SLIDING_EXPIRATION_MINUTES)); // Adjust expiration time as needed
            return data;
        }

        private async Task<IEnumerable<T>> GetAllCachedAsync(string key, Func<Task<IEnumerable<T>>> retrieveData)
        {
            if (_cache.TryGetValue(key, out IEnumerable<T> cachedData))
            {
                return cachedData;
            }

            var data = await retrieveData();
            _cache.Set(key, data, TimeSpan.FromMinutes(5)); // Adjust expiration time as needed
            return data;
        }

        private void RemoveFromCache(object key)
        {
            _cache.Remove(key);
        }

        private void SetCache(object key, T item, TimeSpan? expirationTime = null)
        {
            _cache.Set(key, item, expirationTime ?? TimeSpan.FromMinutes(SET_ABSOLUTE_EXPIRATION_MINUTES));
        }
        */

        private MemoryCacheEntryOptions memoryCacheEntryOptions()
        {
            return new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(SET_SLIDING_EXPIRATION_MINUTES))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(SET_ABSOLUTE_EXPIRATION_MINUTES))
                .SetPriority(CacheItemPriority.Normal);
        }
        #endregion 

        #region Repository Methods
        public async Task<IEnumerable<T>> CachedGetAllAsync(CancellationToken cancellationToken = default)
        {
            var cachekey = $"GetAll:{typeof(T).FullName}";
            if (!_cache.TryGetValue(cachekey, out IEnumerable<T>? entities))
            {
                entities = await _dbSet.ToListAsync(cancellationToken);
                _cache.Set(cachekey, entities, _memoryCacheEntryOptions);
            }
            return entities;
        }

        public async Task<T> CachedGetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var cachekey = $"GetbyId:{typeof(T).FullName},Id:{id}";
            if (!_cache.TryGetValue(cachekey, out T? entitie))
            {
                entitie = await _dbSet.FindAsync(id, cancellationToken);
                _cache.Set(cachekey, entitie, _memoryCacheEntryOptions);
            }
            return entitie;
        }
        #endregion

        #region Repository Methods With No Tracking
        public async Task<IEnumerable<T>> CachedGetAllWithNoTrackingAsync(CancellationToken cancellationToken = default)
        {
            var cachekey = $"GetAll:{typeof(T).FullName}";
            if (!_cache.TryGetValue(cachekey, out IEnumerable<T>? entities))
            {
                //IQueryable<T> query = _context.Set<T>().AsNoTracking();
                entities =
                    (await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken))
                    .AsEnumerable();
                _cache.Set(cachekey, entities, _memoryCacheEntryOptions);
            }
            return entities;
        }

        public async Task<T> CachedGetByIdWithNoTrackingAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var cachekey = $"GetbyId:{typeof(T).FullName},Id:{id}";
            if (!_cache.TryGetValue(cachekey, out T? entitie))
            {
                IQueryable<T> query = _context.Set<T>().AsNoTracking();
                entitie = await query.FirstOrDefaultAsync(e=> e.Id.Equals(id), cancellationToken);
                var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(SET_SLIDING_EXPIRATION_MINUTES))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(SET_ABSOLUTE_EXPIRATION_MINUTES))
                .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cachekey, entitie, cacheOptions);
            }
            return entitie;
        }
        #endregion

        #region Methods as SeparateQ & NoTracking
        public async Task<IEnumerable<T>> CachedGetAllWithNoTrackingWhereAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            var cachekey = $"GetAllWithNoTrackingWhere:{typeof(T).FullName}";
            if (!_cache.TryGetValue(cachekey, out IEnumerable<T>? entities))
            {
                IQueryable<T> query =  _dbSet.AsNoTracking();
                
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
                query = query.Where(predicate);

                //if (includes.Length > 2) query.AsSplitQuery();

                entities = (await query.ToListAsync(cancellationToken)).AsEnumerable();
                _cache.Set(cachekey, entities, _memoryCacheEntryOptions);
            }
            return entities;
        }
        #endregion
    }
}
