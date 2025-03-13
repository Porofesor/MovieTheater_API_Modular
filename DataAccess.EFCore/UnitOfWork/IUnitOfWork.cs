namespace DataAccess.EFCore.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //IDeveloperRepository Developers { get; }
        //IProjectRepository Projects { get; }
        int Complete();
    }
}
