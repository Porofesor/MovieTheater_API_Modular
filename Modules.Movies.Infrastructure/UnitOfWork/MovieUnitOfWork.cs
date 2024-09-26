using DataAccess.EFCore.UnitOfWork;
using DataAccess.MemoryCaching.RepositoryPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Modules.Movies.Core.Entities;
using Modules.Movies.Infrastructure.Persistence;
using Modules.Movies.Infrastructure.Repository;

namespace Modules.Movies.Infrastructure.UnitOfWork
{
    public class MovieUnitOfWork : UnitOfWork<MoviesDbContext>,IMovieUnitOfWork,IDisposable
    {
        private readonly MoviesDbContext _context;
        private readonly IMemoryCache _cache;
        public MovieUnitOfWork(MoviesDbContext context, IMemoryCache cache) : base(context)
        {
            _context = context;
            _cache = cache;
            // Eager Loading
            MovieRepository = new MovieRepository(context);
        }
        public IMovieRepository MovieRepository { get; set; }
        private ICachingRepository<Movie, int> cachingRepository { get; set; } //Lazy Loading
        public ICachingRepository<Movie, int> MovieCachingRepository => cachingRepository ??= new CachingRepository<Movie, int, MoviesDbContext>(_cache, _context);
    }
}
