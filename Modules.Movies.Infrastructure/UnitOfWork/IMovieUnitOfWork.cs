using DataAccess.EFCore.BaseRepository;
using Modules.Movies.Core.Entities;
using Modules.Movies.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Movies.Infrastructure.UnitOfWork
{
    public interface IMovieUnitOfWork 
    {
        IMovieRepository MovieRepository { get; }
        int Complete();
    }
}
