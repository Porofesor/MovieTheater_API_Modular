using DataAccess.RPDecorator.Repository.Generics;
using DataAccess.RPDecorator.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.Metrics;

namespace DataAccess.RPDecorator.Repository.Cached
{
    public class CachedRepository<TEntity> : GeneralRepository<TEntity>, ICachedRepository<TEntity>
    where TEntity : class
    {
        private readonly IMemoryCache _cache;

        public CachedRepository(DbContext context, IMemoryCache cache) : base(context)
        {
            _cache = cache;
        }

        public TEntity GetCachedById(object id)
        {
            var cacheKey = $"{typeof(TEntity).Name}_{id}";
            if (!_cache.TryGetValue(cacheKey, out TEntity entity))
            {
                entity = _context.Set<TEntity>().Find(id);
                _cache.Set(cacheKey, entity, TimeSpan.FromMinutes(5)); // Cache for 5 minutes
            }
            return entity;
        }
    }
}
