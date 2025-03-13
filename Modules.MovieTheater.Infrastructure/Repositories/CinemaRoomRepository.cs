using DataAccess.EFCore.BaseRepository;
using Modules.MovieTheater.Core.Abstractions.Repositories;
using Modules.MovieTheater.Core.Entities;
using Modules.MovieTheater.Infrastructure.Persistance;

namespace Modules.MovieTheater.Infrastructure.Repositories
{
    public class CinemaRoomRepository : GenericRepository<CinemaRoom,MovieTheaterDbContext>, ICinemaRoomRepository
    {
        public CinemaRoomRepository(MovieTheaterDbContext context) : base(context) { }  
    }
}
