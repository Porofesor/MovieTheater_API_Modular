using DataAccess.EFCore.Extends;
using DataAccess.RPDecorator.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.RPDecorator.Repository
{
    public class EntityRepository<T, TKey, CustomDbContext> : GeneralRepository<T>
        where T : class, IEntity<TKey>
        where TKey : struct
        where CustomDbContext : DbContext
    {
        protected readonly CustomDbContext _context; //ModuleDbContext
        private readonly DbSet<T> _dbSet;
        public EntityRepository(CustomDbContext context):base(context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

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
    }
}
