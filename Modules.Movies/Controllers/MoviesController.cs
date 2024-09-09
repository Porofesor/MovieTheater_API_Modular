using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Movies.Core.Entities;
using Modules.Movies.Core.ReqestModels;
using Modules.Movies.Infrastructure.UnitOfWork;

namespace Modules.Movies.Controllers
{
    [ApiController]
    [Route("/api/movies/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMovieUnitOfWork _unitOfWork;
        public MoviesController(IMediator mediator, IMovieUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = _unitOfWork.MovieRepository.GetAll();
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
