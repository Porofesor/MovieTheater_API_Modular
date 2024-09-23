using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Modules.Movies.Core.Entities;
using Modules.Movies.Core.ReqestModels;
using Modules.Movies.Infrastructure.UnitOfWork;

namespace Modules.Movies.Controllers
{
    /// <summary>
    /// Manages movie-related actions
    /// </summary>
    [ApiController]
    [Route("/api/movies/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMovieUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;
        public MoviesController(IMediator mediator, IMovieUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _memoryCache = cache;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Get list of all movies
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of all movies in the system.
        /// </remarks>
        /// <response code="200">Returns the list of movies</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _unitOfWork.MovieCachingRepository.CachedGetAllWithNoTrackingAsync();
            return Ok(movies);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterMovieModel command)
        {
            Movie newMovie = new Movie{
                CreatedDate = DateTime.Now,
                Name = command.Name,
                Description = command.Description,
            };
            _unitOfWork.MovieRepository.Add(newMovie);
            var result = _unitOfWork.Complete();
            return Ok(result);
        }
    }
}
