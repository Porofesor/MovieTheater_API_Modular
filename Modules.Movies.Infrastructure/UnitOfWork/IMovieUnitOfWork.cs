using DataAccess.MemoryCaching.RepositoryPattern;
using Modules.Movies.Core.Entities;
using Modules.Movies.Infrastructure.Repository;
 
namespace Modules.Movies.Infrastructure.UnitOfWork
{
    public interface IMovieUnitOfWork 
    {
        ICachingRepository<Movie, int> MovieCachingRepository { get; }
        IMovieRepository MovieRepository { get; }
        int Complete();
    }
}
