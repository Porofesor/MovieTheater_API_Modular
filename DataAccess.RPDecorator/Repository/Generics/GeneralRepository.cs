using DataAccess.RPDecorator.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.RPDecorator.Repository.Generics
{
    public class GeneralRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
    {
        protected readonly DbContext _context;

        public GeneralRepository(DbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
