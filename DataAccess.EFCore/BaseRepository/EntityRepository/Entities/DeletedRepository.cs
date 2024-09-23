using DataAccess.EFCore.Extends;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.BaseRepository.EntityRepository.Entities
{
    public abstract class DeletedRepository<T, CustomDbContext>
        where T : class, IIsDeleted
        where CustomDbContext : DbContext
    {
        protected readonly CustomDbContext _context; //ModuleDbContext
        // Constructor in the abstract class
        protected DeletedRepository(CustomDbContext context)
        {
            _context = context;
        }
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
    }
}
