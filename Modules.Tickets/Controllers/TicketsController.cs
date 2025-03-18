using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Tickets.Core.Commands;
using Modules.Tickets.Core.Queries;

namespace Modules.Tickets.Controllers
{
    /// <summary>
    /// Manages Ticket actions
    /// </summary>
    [ApiController]
    [Route("/api/tickets/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        ///     Get list of all Tickets
        /// </summary>
        /// <remarks>
        ///     This endpoint returns a list of all Tickets in the system.
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());
            return Ok(tickets);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterTicketsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
