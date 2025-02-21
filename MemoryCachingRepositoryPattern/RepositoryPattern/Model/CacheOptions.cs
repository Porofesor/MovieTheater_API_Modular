using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.MemoryCaching.RepositoryPattern.Model
{
    /// <summary>
    /// Constructor for configuration options for cache expiration and priority settings.
    /// </summary>
    public static class CacheOptions
    {
        /// <summary>
        /// The Constructor for memory cache options
        /// with specified sliding expiration, absolute expiration, and cache priority.
        /// </summary>
        /// <param name="slidingExpirationInMinutes">
        /// The sliding expiration time in minutes. Resets the expiration timer upon access.
        /// </param>
        /// <param name="absoluteExpirationInMinutes">
        /// The absolute expiration time in minutes. Maximum lifetime of the cached item.
        /// </param>
        /// <param name="cachePriority">
        /// The priority level for cache eviction. Determines how likely the item is to be removed.
        /// </param>
        public static MemoryCacheEntryOptions CacheOptionConstructor(int slidingExpirationInMinutes, int absoluteExpirationInMinutes, CacheItemPriority cachePriority)
        {
            return new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpirationInMinutes))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(absoluteExpirationInMinutes))
                .SetPriority(cachePriority);
        }
    }
}
