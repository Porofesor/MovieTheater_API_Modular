using DataAccess.EFCore.BaseRepository;
using Modules.Movies.Core.Entities;

namespace Modules.Movies.Infrastructure.Repository
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
    }
}
