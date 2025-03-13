using DataAccess.MemoryCaching.RepositoryPattern.Model;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.MemoryCaching.RepositoryPattern
{
    public class CachingRepository<T> : ICachingRepository<T>
        where T : class
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingRepository{T}"/> class 
        /// with the specified memory cache and optional cache configuration.
        /// </summary>
        /// <param name="cache">The memory cache instance for caching operations.</param>
        /// <param name="cacheOptions">
        /// Optional cache configuration. If not provided, default options are used.
        /// </param>
        public CachingRepository(IMemoryCache cache, MemoryCacheEntryOptions cacheOptions = null)
        {
            _cache = cache;
            //_repository = repository;
            _memoryCacheEntryOptions = cacheOptions ?? new MemoryCacheEntryOptions()
                                                            .SetSlidingExpiration(TimeSpan.FromSeconds(5))
                                                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(5))
                                                            .SetPriority(CacheItemPriority.Normal); 
        }

        #region Universal
        /// <summary>
        /// Retrieves data from the cache using the specified key.
        /// If the key does not exist in the cache, the method returns null.
        /// This function is designed to serve as a universal cache retrieval mechanism,
        /// simplifying data access within the controller.
        /// </summary>
        /// <typeparam name="T">The type of the data being retrieved.</typeparam>
        /// <param name="cachekey">The unique key used to locate the cached data.</param>
        /// <returns>
        /// An enumerable collection of type <typeparamref name="T"/> if the cache contains the specified key; 
        /// otherwise, <c>null</c>.
        /// </returns>
        public async Task<List<T>> GetFromCacheOrNull(string cachekey)
        {
            if (!_cache.TryGetValue(cachekey, out List<T>? entities))
            {
                return null;
            }
            return entities;
        }

        /// <summary>
        /// Adds a list of entities to the cache using the specified cache key.
        /// If no cache entry options are provided, default options are used.
        /// </summary>
        /// <param name="cachekey">
        /// The unique key used to store the cached data.
        /// </param>
        /// <param name="entities">
        /// The list of entities to be cached.
        /// </param>
        /// <param name="cacheEntryOptions">
        /// Optional cache entry options to control expiration and priority. If not provided, default options are applied.
        /// </param>
        public async Task AddToCache(string cachekey, List<T> entities, MemoryCacheEntryOptions? cacheEntryOptions = null)
        {
            _cache.Set(cachekey, entities, cacheEntryOptions ?? _memoryCacheEntryOptions);
        }

        #endregion

        #region basic methods for caching

        /// <summary>
        /// Retrieves data from the cache if it exists; otherwise, retrieves the data using the provided function,
        /// saves it to the cache, and then returns it.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve and cache.</typeparam>
        /// <param name="key">The cache key associated with the data.</param>
        /// <param name="retrieveData">A function to retrieve the data if it is not found in the cache.</param>
        /// <param name="cacheEntryOptions">
        /// Optional cache entry options to configure cache behavior such as expiration.
        /// If not provided, default options will be used.
        /// </param>
        /// <returns>A task representing the asynchronous operation. The result contains the retrieved data.</returns>
        public async Task<List<T>> GetCachedAndSave(string key, Func<Task<List<T>>> retrieveData, MemoryCacheEntryOptions? cacheEntryOptions = null)
        {
            if (_cache.TryGetValue(key, out List<T> cachedData))
            {
                return cachedData;
            }

            var data = await retrieveData();
            //_cache.Set(key, data, cacheEntryOptions ?? _memoryCacheEntryOptions); // Adjust expiration time as needed

            // Async Save to cache
            AddToCache(key, data, cacheEntryOptions ?? _memoryCacheEntryOptions);

            #region "Fire and forget" approach
            // Cache the data in the background PS: This is the only use case for "Fire and forget" pattern (i think).
            //_ = Task.Run(() =>
            //{
            //    _cache.Set(key, data, cacheEntryOptions ?? _memoryCacheEntryOptions); // Adjust expiration time as needed
            //});
            #endregion 

            return data;
        }

        public void RemoveFromCache(object key)
        {
            _cache.Remove(key);
        }
        #endregion


        #region Commented code
        //private void SetCache(object key, T item, TimeSpan? expirationTime = null)
        //{
        //    _cache.Set(key, item, expirationTime ?? TimeSpan.FromMinutes(SET_ABSOLUTE_EXPIRATION_MINUTES));
        //}
        #region Depricated Basic methods
        // Unnessesary optimalization made by author, made for fun and tests

        //private async Task CacheElements(string cachekey, Task<IEnumerable<T>> entities, MemoryCacheEntryOptions memoryCacheEntryOptions)
        //{
        //    _cache.Set(cachekey, await entities, memoryCacheEntryOptions);
        //}

        //public async Task<T> GetCachedAndSave(object key, Func<Task<T>> retrieveData, MemoryCacheEntryOptions? cacheEntryOptions = null)
        //{
        //    if (_cache.TryGetValue(key, out T cachedData))
        //    {
        //        return cachedData;
        //    }

        //    var data = await retrieveData();
        //    _cache.Set(key, data, cacheEntryOptions ?? _memoryCacheEntryOptions); // Adjust expiration time as needed
        //    return data;
        //}

        #endregion

        #region Repository Methods
        //public async Task<IEnumerable<T>> CachedGetAllAsync(CancellationToken cancellationToken = default)
        //{
        //    var cachekey = $"GetAll:{typeof(T).FullName}";
        //    if (!_cache.TryGetValue(cachekey, out IEnumerable<T>? entities))
        //    {
        //        entities = _repository.GetAll(); //TODO
        //        _cache.Set(cachekey, entities, _memoryCacheEntryOptions);
        //    }
        //    return entities;
        //}

        ////TODO TESTING , delete pls
        ////public async Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        ////    where TId : struct
        ////{
        ////    // Example of caching based on IEntity<> behavior
        ////    if (IsEntity())
        ////    {
        ////        var cacheKey = $"{typeof(T).Name}-{id}";
        ////        if (!_cache.TryGetValue(cacheKey, out T cachedItem))
        ////        {
        ////            cachedItem = await _repository.GetById(id, cancellationToken);
        ////            if (cachedItem != null)
        ////            {
        ////                _cache.Set(cacheKey, cachedItem);
        ////            }
        ////        }
        ////        return cachedItem;
        ////    }

        ////    // For non-IEntity<T> classes, directly fetch from the repository
        ////    return await _repository.GetById(id, cancellationToken);
        ////}

        ////TODO TESTING , delete pls
        //private bool IsEntity()
        //{
        //    // Check if T implements IEntity<>
        //    return typeof(T).GetInterfaces()
        //        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntity<>));
        //}

        //public async Task<T> CachedGetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        //{
        //    var cachekey = $"GetbyId:{typeof(T).FullName},Id:{id}";
        //    if (!_cache.TryGetValue(cachekey, out T? entitie))
        //    {
        //        entitie = await _dbSet.FindAsync(id, cancellationToken);
        //        _cache.Set(cachekey, entitie, _memoryCacheEntryOptions);
        //    }
        //    return entitie;
        //}
        #endregion

        #region Repository Methods With No Tracking
        //public async Task<IEnumerable<T>> CachedGetAllWithNoTrackingAsync(CancellationToken cancellationToken = default)
        //{
        //    var cachekey = $"GetAll:{typeof(T).FullName}";
        //    if (!_cache.TryGetValue(cachekey, out IEnumerable<T>? entities))
        //    {
        //        //IQueryable<T> query = _context.Set<T>().AsNoTracking();
        //        entities =
        //            (await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken));
        //        _cache.Set(cachekey, entities, _memoryCacheEntryOptions);
        //    }
        //    return entities;
        //}

        //public async Task<T> CachedGetByIdWithNoTrackingAsync(TKey id, CancellationToken cancellationToken = default)
        //{
        //    var cachekey = $"GetbyId:{typeof(T).FullName},Id:{id}";
        //    if (!_cache.TryGetValue(cachekey, out T? entitie))
        //    {
        //        IQueryable<T> query = _context.Set<T>().AsNoTracking();
        //        entitie = await query.FirstOrDefaultAsync(e=> e.Id.Equals(id), cancellationToken);
        //        var cacheOptions = new MemoryCacheEntryOptions()
        //        .SetSlidingExpiration(TimeSpan.FromSeconds(SET_SLIDING_EXPIRATION_MINUTES))
        //        .SetAbsoluteExpiration(TimeSpan.FromSeconds(SET_ABSOLUTE_EXPIRATION_MINUTES))
        //        .SetPriority(CacheItemPriority.Normal);
        //        _cache.Set(cachekey, entitie, cacheOptions);
        //    }
        //    return entitie;
        //}
        #endregion

        #region Methods as SeparateQ & NoTracking
        //public async Task<IEnumerable<T>> CachedGetAllWithNoTrackingWhereAsync(
        //    Expression<Func<T, bool>> predicate,
        //    CancellationToken cancellationToken = default,
        //    params Expression<Func<T, object>>[] includes)
        //{
        //    var cachekey = $"GetAllWithNoTrackingWhere:{typeof(T).FullName}";
        //    if (!_cache.TryGetValue(cachekey, out IEnumerable<T>? entities))
        //    {
        //        IQueryable<T> query =  _dbSet.AsNoTracking();

        //        foreach(var include in includes)
        //        {
        //            query = query.Include(include);
        //        }
        //        query = query.Where(predicate);

        //        //if (includes.Length > 2) query.AsSplitQuery();

        //        entities = (await query.ToListAsync(cancellationToken)).AsEnumerable();
        //        _cache.Set(cachekey, entities, _memoryCacheEntryOptions);
        //    }
        //    return entities;
        //}
        #endregion
        #endregion
    }
}
