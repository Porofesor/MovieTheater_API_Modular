using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.MovieTheater.Core.Abstractions;
using Modules.MovieTheater.Core.Entities;

namespace Modules.MovieTheater.Core.Commands.Movies
{
    public class RegisterMovieCommand: IRequest<Movie>
    {
        public string Name { get; set; }
        public bool IsDeleted = false;
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    internal class MovieCommandHandler : IRequestHandler<RegisterMovieCommand, Movie>
    {
        private readonly IMovieTheaterUnitOfWork _unitofwork;
        public MovieCommandHandler(IMovieTheaterUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task<Movie?> Handle(RegisterMovieCommand command, CancellationToken cancellationToken)
        {
            if (await _unitofwork.MovieRepository.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                return null;
            }
            var brand = new Movie { Description = command.Description, Name = command.Name, CreatedDate = command.CreatedDate, IsDeleted = command.IsDeleted};
            await _unitofwork.MovieRepository.AddAsync(brand, cancellationToken);
            await _unitofwork.CompleteAsync();
            return brand;
        }
    }
}
