using DataAccess.EFCore.Extends;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.EFCore.BaseRepository.EntityRepository.Entities
{
    public abstract class EntityDeletedRepository<T,TKey, CustomDbContext>
        where T : class, IIsDeleted, IEntity<TKey>
        where CustomDbContext : DbContext
        where TKey : struct
    {
        protected readonly CustomDbContext _context; //ModuleDbContext
        private readonly DbSet<T> _dbSet;
        public EntityDeletedRepository(CustomDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #region IsDeleted
        public void Remove(T entity)
        {
            // Soft delete
            entity.IsDeleted = true;
            _context.Set<T>().Update(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }
            _context.Set<T>()
                .UpdateRange(entities);
        }
        #endregion

        #region IEntity
        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T GetById(TKey id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<IEnumerable<T>> GetAllByIdWithNoTrackingAsync(
            IEnumerable<TKey> ids,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllByIdWithNoTrackingWhereAsync(
            IEnumerable<TKey> ids,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdWithNoTrackingAsync(
            TKey id,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => id.Equals(e.Id), cancellationToken);
        }
        #endregion
    }
}
