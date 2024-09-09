using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Tickets.Core.Abstractions;
using Modules.Tickets.Core.Entities;

namespace Modules.Tickets.Core.Commands
{
    // Commands: POST PUT DELETE
    public class RegisterTicketsCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Detail { get; set; }
    }
    internal class TicketCommandHandler : IRequestHandler<RegisterTicketsCommand, int>
    {
        private readonly ITicketDbContext _context;
        public TicketCommandHandler(ITicketDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(RegisterTicketsCommand command, CancellationToken cancellationToken)
        {
            if (await _context.Tickets.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                throw new Exception("Brand with the same name already exists.");
            }
            var brand = new Ticket { Detail = command.Detail, Name = command.Name };
            await _context.Tickets.AddAsync(brand, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return brand.Id;
        }
    }
}
