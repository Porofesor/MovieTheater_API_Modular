using Modules.Movies.Infrastructure.Repository;
 
namespace Modules.Movies.Infrastructure.UnitOfWork
{
    public interface IMovieUnitOfWork 
    {
        IMovieRepository MovieRepository { get; }
        int Complete();
    }
}
