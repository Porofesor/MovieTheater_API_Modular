using DataAccess.EFCore.BaseRepository;
using DataAccess.MemoryCaching.RepositoryPattern;
using Microsoft.Extensions.Caching.Memory;
using Modules.Movies.Core.Entities;
using Modules.Movies.Infrastructure.Persistence;

namespace Modules.Movies.Infrastructure.Repository
{
    public class MovieRepository : GenericRepository<Movie, MoviesDbContext>, IMovieRepository
    {
        private ICachingRepository<Movie, int>? cachingRepository;
        //private readonly IMemoryCache _cache;
        //public ICachingRepository<Movie, int> MovieCachingRepository => cachingRepository ??= new CachingRepository<Movie,int,MoviesDbContext>(_cache, _context);
        public MovieRepository(MoviesDbContext context) :base(context) 
        {
            //_cache = cache;
        }
    }
}
