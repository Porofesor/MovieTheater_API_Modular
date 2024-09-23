using DataAccess.EFCore.Extends;
using DataAccess.RPDecorator.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.RPDecorator.Repository
{
    public class SoftDeletedRepository<T, CustomDbContext> : GeneralRepository<T>
        where T : class, IIsDeleted
        where CustomDbContext : DbContext
    {
        protected readonly CustomDbContext _context; //ModuleDbContext
        // Constructor in the abstract class
        public SoftDeletedRepository(CustomDbContext context):base(context) 
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
