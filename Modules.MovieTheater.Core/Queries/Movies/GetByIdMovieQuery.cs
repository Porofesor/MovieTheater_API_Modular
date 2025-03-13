using MediatR;
using Modules.MovieTheater.Core.Abstractions;
using Modules.MovieTheater.Core.Entities;

namespace Modules.MovieTheater.Core.Queries.Movies
{
    public class GetByIdMovieQuery : IRequest<Movie>
    {
        public int Id { get; set; }
    }
    internal class GetByIdMovieQueryHandler : IRequestHandler<GetByIdMovieQuery, Movie>
    {
        private readonly IMovieTheaterUnitOfWork _unitOfWork;
        public GetByIdMovieQueryHandler(IMovieTheaterUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Movie> Handle(GetByIdMovieQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.MovieRepository.GetByIdAsync<Movie,int>(request.Id);
        }
    }
}
