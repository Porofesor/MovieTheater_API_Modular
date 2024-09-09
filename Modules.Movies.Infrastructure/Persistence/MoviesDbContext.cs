using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Modules.Movies.Core.Abstractions;
using Modules.Movies.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Modules.Movies.Infrastructure.Persistence
{
    //update-database -context MoviesDbContext
    //add-migration initial -context MoviesDbContext
    public class MoviesDbContext : ModuleDbContext, IMoviesDbContext
    {
        protected override string Schema => "Movies";
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
