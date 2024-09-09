namespace DataAccess.MemoryCaching.RepositoryPattern
{
    public interface ICachingRepository<T> where T : class
    {
        Task<T> GetCachedAsync(object key, Func<Task<T>> retrieveData);
        Task<IEnumerable<T>> GetAllCachedAsync(string key, Func<Task<IEnumerable<T>>> retrieveData);
        void RemoveFromCache(object key);
        void SetCache(object key, T item, TimeSpan? expirationTime = null);
    }
}
