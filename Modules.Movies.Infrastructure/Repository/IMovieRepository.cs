using DataAccess.EFCore.BaseRepository;
using Modules.Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Movies.Infrastructure.Repository
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
    }
}
