using DataAccess.EFCore.Extends;

namespace DataAccess.RPDecorator.Repository.Interfaces
{
    public interface IEntityRepository<TEntity, TKey> : IRepository<TEntity>
    where TEntity : IEntity<TKey>
    {
        TEntity GetById(TKey id);
    }
}
