using Microsoft.EntityFrameworkCore;
using Modules.Tickets.Core.Entities;

namespace Modules.Tickets.Core.Abstractions
{
    public interface ITicketDbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
