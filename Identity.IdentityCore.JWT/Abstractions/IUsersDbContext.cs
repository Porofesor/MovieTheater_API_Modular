using Identity.IdentityCore.JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.IdentityCore.JWT.Abstractions
{
    public interface IUsersDbContext
    {
        public DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
