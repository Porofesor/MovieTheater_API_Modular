using Microsoft.EntityFrameworkCore;
using Modules.MovieTheater.Core.Abstractions;
using Modules.MovieTheater.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Modules.MovieTheater.Infrastructure.Persistance
{
    public class MovieTheaterDbContext : ModuleDbContext, IMovieTheaterDbContext
    {
        protected override string Schema => "MovieTheater";
        public MovieTheaterDbContext(DbContextOptions<MovieTheaterDbContext> options) : base(options)
        {
        }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<CinemaRoom> CinemaRoom { get; set; }
        public DbSet<Screening> Screening { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<User> Users { get; set; }  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
        }
    }
}
