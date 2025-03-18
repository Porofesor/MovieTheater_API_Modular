using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.MovieTheater.Core.Commands.Movies;
using Modules.MovieTheater.Core.Queries.Movies;

namespace Modules.MovieTheater.Controllers
{
    /// <summary>
    /// Manages movie-related actions
    /// </summary>
    [ApiController]
    [Route("/api/MovieTheater/[controller]/[action]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        ///     (new controller) Get list of all movies
        /// </summary>
        /// <remarks>
        ///     This endpoint returns a list of all movies in the system.
        /// </remarks>
        /// <response code="200">Returns the list of movies</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _mediator.Send(new GetAllMoviesQuery());
            return Ok(movies);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            var movies = await _mediator.Send(new GetByIdMovieQuery{Id = Id});
            if(movies is null) return NotFound(new { message = $"Product with ID {Id} not found." });
            return Ok(movies);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateMovie(RegisterMovieCommand command)
        {
            var movie = await _mediator.Send(command);
            if (movie is null) return NotFound(new { message = $"Product with ID {Id} not found." });
            return Ok(movie);
        }
    }
}
