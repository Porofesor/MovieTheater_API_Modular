using DataAccess.EFCore.Extends;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.EFCore.BaseRepository
{
    public class GenericRepository<T, CustomDbContext> : IGenericRepository<T> //, TId, TIsDeleted
        where T : class
        where CustomDbContext : DbContext
        //where TId : class, IEntity<int>
        //where TIsDeleted : class, IIsDeleted
    {
        protected readonly CustomDbContext _context; //ModuleDbContext
        private readonly DbSet<T> _dbSet;
        public GenericRepository(CustomDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #region basic methods
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        { 
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync<TEntity, TKey>(TKey id)
        where TEntity : class, IEntity<TKey>
        {
            // Use the Find method to get the entity by ID
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public TEntity GetById<TEntity, TKey>(TKey id)
        where TEntity : class, IEntity<TKey>
        {
            // Use the Find method to get the entity by ID
            return _context.Set<TEntity>().Find(id);
        }

        public void Remove(T entity)
        {
            if (entity is IIsDeleted deletable)
            {
                // Soft delete
                deletable.IsDeleted = true;
                _context.Set<T>().Update(entity);
            }
            else
            {
                // Hard delete
                _context.Set<T>().Remove(entity);
            }
        }

        private void HardDelete(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        private void SoftDelete<T>(IEnumerable<T> entities) where T : class, IIsDeleted
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }
            _context.Set<T>()
                .UpdateRange(entities);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            if (entities is IIsDeleted)
                SoftDelete(entities.OfType<IIsDeleted>().ToList()); // TODO Had no better idea
            else
                HardDelete(entities);
        }
        #endregion
        // ------------------------------
        #region GetAll NoTracking Async
        public async Task<IEnumerable<TId>> GetAllByIdWithNoTrackingAsync<TId, TKey>(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default,
        params Expression<Func<TId, object>>[] includes)
        where TId : class, IEntity<TKey>
        where TKey : struct
        {
            IQueryable<TId> query = _context.Set<TId>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TId>> GetAllByIdWithNoTrackingWhereAsync<TId, TKey>(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default,
        params Expression<Func<TId, object>>[] includes)
        where TId : class, IEntity<TKey>
            where TKey : struct
        {
            IQueryable<TId> query = _context.Set<TId>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
        }
        public async Task<TId?> GetByIdWithNoTrackingAsync<TId, TKey>(
        TKey id,
        CancellationToken cancellationToken = default,
        params Expression<Func<TId, object>>[] includes)
        where TId : class, IEntity<TKey>
        where TKey : struct
        {
            IQueryable<TId> query = _context.Set<TId>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
        }

        // Examples for predicate:
        // var movies = await movieRepository.GetAllWithNoTrackingWhereAsync(
        //    m => m.Year > 2000,
        //    m => m.Director == "Christopher Nolan",
        //    m => m.Reviews,
        //    m => m.Title.Contains("Inception")
        // );
        public async Task<IEnumerable<T>> GetAllWithNoTrackingWhereAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = query.Where(predicate);

            return await query.ToListAsync(cancellationToken);
        }
        #endregion
    }
}
