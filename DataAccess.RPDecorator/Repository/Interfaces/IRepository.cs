namespace DataAccess.RPDecorator.Repository.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Save();
    }
}
