using Identity.IdentityCore.JWT.Abstractions;
using Identity.IdentityCore.JWT.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;

namespace Identity.IdentityCore.JWT.Infrastructure.Persistence
{
    public class UsersDbContext : ModuleDbContext, IUsersDbContext
    {
        protected override string Schema => "Users";
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
