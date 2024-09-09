using DataAccess.EFCore.UnitOfWork;
using Modules.Movies.Infrastructure.Persistence;
using Modules.Movies.Infrastructure.Repository;

namespace Modules.Movies.Infrastructure.UnitOfWork
{
    public class MovieUnitOfWork : UnitOfWork<MoviesDbContext>,IMovieUnitOfWork
    {
        private readonly MoviesDbContext _context;
        public MovieUnitOfWork(MoviesDbContext context) : base(context)
        {
            _context = context;
            // Eager Loading
            MovieRepository = new MovieRepository(context);
        }
        public IMovieRepository MovieRepository { get; set; }
    }
}
