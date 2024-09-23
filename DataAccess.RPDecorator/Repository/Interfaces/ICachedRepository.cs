namespace DataAccess.RPDecorator.Repository.Interfaces
{
    public interface ICachedRepository<TEntity> : IRepository<TEntity>
    {
        TEntity GetCachedById(object id);
    }
}
