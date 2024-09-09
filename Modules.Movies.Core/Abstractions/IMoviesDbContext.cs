using Microsoft.EntityFrameworkCore;
using Modules.Movies.Core.Entities;

namespace Modules.Movies.Core.Abstractions
{
    public interface IMoviesDbContext
    {
        public DbSet<Movie> Movies { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
