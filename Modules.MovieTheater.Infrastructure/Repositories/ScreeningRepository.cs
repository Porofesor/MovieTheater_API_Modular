using DataAccess.EFCore.BaseRepository;
using Modules.MovieTheater.Core.Abstractions.Repositories;
using Modules.MovieTheater.Core.Entities;
using Modules.MovieTheater.Infrastructure.Persistance;

namespace Modules.MovieTheater.Infrastructure.Repositories
{
    public class ScreeningRepository : GenericRepository<Screening, MovieTheaterDbContext>, IScreeningRepository
    {
        public ScreeningRepository(MovieTheaterDbContext context) : base(context) { }   
    }
}
