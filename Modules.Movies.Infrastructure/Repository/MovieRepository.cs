using DataAccess.EFCore.BaseRepository;
using DataAccess.EFCore.Extends;
using Modules.Movies.Core.Entities;
using Modules.Movies.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Movies.Infrastructure.Repository
{
    public class MovieRepository : GenericRepository<Movie, MoviesDbContext>, IMovieRepository
    {
        public MovieRepository(MoviesDbContext context):base(context) { }
    }
}
