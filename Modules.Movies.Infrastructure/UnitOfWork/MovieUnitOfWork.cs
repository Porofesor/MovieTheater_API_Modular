using DataAccess.EFCore.UnitOfWork;
using Microsoft.Extensions.Caching.Memory;
using Modules.Movies.Infrastructure.Persistence;
using Modules.Movies.Infrastructure.Repository;

namespace Modules.Movies.Infrastructure.UnitOfWork
{
    public class MovieUnitOfWork : UnitOfWork<MoviesDbContext>,IMovieUnitOfWork
    {
        private readonly MoviesDbContext _context;
        private readonly IMemoryCache _cache;
        public MovieUnitOfWork(MoviesDbContext context, IMemoryCache cache) : base(context)
        {
            _context = context;
            _cache = cache;
            // Eager Loading
            MovieRepository = new MovieRepository(context,cache);
        }
        public IMovieRepository MovieRepository { get; set; }
    }
}
