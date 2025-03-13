namespace DataAccess.EFCore.Extends
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
}
