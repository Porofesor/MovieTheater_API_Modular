using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Modules.MovieTheater.Controllers
{
    [ApiController]
    [Route("/api/MovieTheater/[controller]")]
    public class MovieTheaterController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MovieTheaterController(IMediator mediator)
        {
            _mediator = mediator;
        }

    }
}
