using DataAccess.EFCore.Extends;
using System.Linq.Expressions;

namespace DataAccess.EFCore.BaseRepository
{
    public interface IGenericRepository<T> where T : class
    {
        //T GetById(int id);
        #region Basic methods
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        //T GetById<TId>(int id) where TId : struct;
        TEntity GetById<TEntity, TKey>(TKey id)
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
        Task<IEnumerable<T>> GetAllWithNoTrackingWhereAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);
        #endregion
    }
}
