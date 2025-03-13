using MediatR;
using Modules.MovieTheater.Core.Abstractions;
using Modules.MovieTheater.Core.Entities;

namespace Modules.MovieTheater.Core.Queries.Movies
{
    public class GetAllMoviesQuery : IRequest<IEnumerable<Movie>>
    {
    }
    internal class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<Movie>>
    {
        private readonly IMovieTheaterUnitOfWork _unitOfWork;
        public GetAllMoviesQueryHandler(IMovieTheaterUnitOfWork MovieTheaterUnitOfWork)
        {
            _unitOfWork = MovieTheaterUnitOfWork;
        }
        public async Task<IEnumerable<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.MovieRepository.GetAllAsync(cancellationToken);
        }
    }
}
