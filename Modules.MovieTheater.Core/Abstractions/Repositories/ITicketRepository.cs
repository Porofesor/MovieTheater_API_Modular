using DataAccess.EFCore.BaseRepository;
using Modules.MovieTheater.Core.Entities;

namespace Modules.MovieTheater.Core.Abstractions.Repositories
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
    }
}
