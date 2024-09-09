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
        T GetById<TId>(int id) where TId : struct;
        void Add(T entity);
        void AddAsync(T entity, CancellationToken cancellationToken);
        void AddRange(IEnumerable<T> entities); 
        void AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        #endregion

        #region GetAll WithNoTracking Async
        Task<IEnumerable<TId>> GetAllByIdWithNoTrackingAsync<TId>(
            IEnumerable<int> ids,
            CancellationToken cancellationToken,
            params Expression<Func<TId, object>>[] includes)
            where TId : class, IEntity<int>;
        Task<IEnumerable<TId>> GetAllByIdWithNoTrackingWhereAsync<TId>(
            IEnumerable<int> ids,
            CancellationToken cancellationToken,
            params Expression<Func<TId, object>>[] includes)
            where TId : class, IEntity<int>;
        Task<TId?> GetByIdWithNoTrackingAsync<TId>(
            int id,
            CancellationToken cancellationToken,
            params Expression<Func<TId, object>>[] includes)
            where TId : class, IEntity<int>;
        Task<IEnumerable<T>> GetAllWithNoTrackingWhereAsync(
            CancellationToken cancellationToken,
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);
        #endregion
    }
}
