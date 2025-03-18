using DataAccess.EFCore.Extends;
using System.Linq.Expressions;

namespace DataAccess.EFCore.BaseRepository
{
    public interface IGenericRepository<T> where T : class
    {
        //T GetById(int id);
        #region Basic methods
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        //T GetById<TId>(int id) where TId : struct;
        TEntity GetById<TEntity, TKey>(TKey id) // TODO ???
        where TEntity : class, IEntity<TKey>;
        public Task<TEntity> GetByIdAsync<TEntity, TKey>(TKey id) // TODO ???
        where TEntity : class, IEntity<TKey>;

        void Add(T entity);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        void AddRange(IEnumerable<T> entities); 
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        #endregion

        #region GetAll WithNoTracking Async
        Task<IEnumerable<TId>> GetAllByIdWithNoTrackingAsync<TId, TKey>(
            IEnumerable<TKey> ids,
            CancellationToken cancellationToken,
            params Expression<Func<TId, object>>[] includes)
            where TId : class, IEntity<TKey>
            where TKey : struct;
        Task<IEnumerable<TId>> GetAllByIdWithNoTrackingWhereAsync<TId, TKey>(
            IEnumerable<TKey> ids,
            CancellationToken cancellationToken,
            params Expression<Func<TId, object>>[] includes)
            where TId : class, IEntity<TKey>
            where TKey : struct;
        Task<TId?> GetByIdWithNoTrackingAsync<TId, TKey>(
            TKey id,
            CancellationToken cancellationToken,
            params Expression<Func<TId, object>>[] includes)
            where TId : class, IEntity<TKey>
            where TKey : struct;
        Task<IEnumerable<T>> GetAllWithNoTrackingWhere(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllWithNoTrackingWhere(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default,
            int top = 100,
            params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllWithNoTrackingWhere(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> orderBy = null, // OrderBy parameter
            bool ascending = true,                      // Ascending/Descending flag
            int top = 100,                              // Limit
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllNoTrackingWherePagination(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> orderBy = null, // OrderBy parameter
            bool ascending = true,                      // Ascending/Descending flag
            int page = 1,                               // Page number
            int pageSize = 10,                          // Page size
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllNoTrackingPaginationAsync(
            Expression<Func<T, object>> orderBy = null, // OrderBy parameter
            bool ascending = true,                      // Ascending/Descending flag
            int page = 1,                               // Page number
            int pageSize = 20,                          // Page size
            CancellationToken cancellationToken = default);
        #endregion
    }
}
