using Microsoft.EntityFrameworkCore;
using Modules.Tickets.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Tickets.Core.Abstractions
{
    public interface ITicketDbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
