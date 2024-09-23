using DataAccess.EFCore.Extends;

namespace DataAccess.RPDecorator.Repository.Interfaces
{
    public interface ISoftDeleteRepository<TEntity> : IRepository<TEntity>
    where TEntity : IIsDeleted
    {
        void SoftDelete(TEntity entity);
        IEnumerable<TEntity> GetAllNotDeleted();
    }
}
