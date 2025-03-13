using DataAccess.EFCore.Extends;

namespace Modules.MovieTheater.Core.Entities
{
    public class User : IEntity<int>, IDeleted
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
