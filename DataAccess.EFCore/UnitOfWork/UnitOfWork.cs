using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.UnitOfWork
{
    public class UnitOfWork<CustomDbContext> : IUnitOfWork
        where CustomDbContext : DbContext
    {
        private bool IsDisposed = false;
        private readonly CustomDbContext _context;
        public UnitOfWork(CustomDbContext context)
        {
            _context = context;
            //Developers = new DeveloperRepository(_context);
            //Projects = new ProjectRepository(_context);
        }
        //public IDeveloperRepository Developers { get; private set; }
        //public IProjectRepository Projects { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    //release managed resources
                    _context.Dispose();// TODO ????
                }
                //release unmanaged resources
                IsDisposed = true;
            }
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
