using DataAccess.EFCore.Extends;
using System.Linq.Expressions;

namespace DataAccess.MemoryCaching.RepositoryPattern
{
    // TODO not sure about ICachingRepository<T,TKey> <= TKey?
    public interface ICachingRepository<T,TKey> 
        where T : class, IEntity<TKey>
        where TKey : struct
    {
        Task<IEnumerable<T>> CachedGetAllAsync(CancellationToken cancellationToken = default);
        Task<T> CachedGetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> CachedGetAllWithNoTrackingAsync(CancellationToken cancellationToken = default);
        Task<T> CachedGetByIdWithNoTrackingAsync(TKey id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> CachedGetAllWithNoTrackingWhereAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);
        //Task<T> GetCachedAsync(object key, Func<Task<T>> retrieveData);
        //Task<IEnumerable<T>> GetAllCachedAsync(string key, Func<Task<IEnumerable<T>>> retrieveData);
        //void RemoveFromCache(object key);
        //void SetCache(object key, T item, TimeSpan? expirationTime = null);
    }
}
