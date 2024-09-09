using Microsoft.EntityFrameworkCore;
using Modules.Tickets.Core.Abstractions;
using Modules.Tickets.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Modules.Tickets.Infrastructure.Persistence
{
    public class TicketDbContext : ModuleDbContext, ITicketDbContext
    {
        protected override string Schema => "Ticket";
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {
        }
        public DbSet<Ticket> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
