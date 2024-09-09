using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Tickets.Core.Abstractions;
using Modules.Tickets.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Tickets.Core.Queries
{
    //QUERY: GET
    public class GetAllTicketsQuery : IRequest<IEnumerable<Ticket>>
    {
    }
    internal class TicketQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<Ticket>>
    {
        private readonly ITicketDbContext _context;
        public TicketQueryHandler(ITicketDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Ticket>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _context.Tickets.OrderBy(x => x.Id).ToListAsync(cancellationToken);
            if (brands == null) throw new Exception("Brands Not Found!");
            return brands; 
        }
    }
}
