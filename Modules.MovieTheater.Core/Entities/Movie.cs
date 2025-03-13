using DataAccess.EFCore.Extends;

namespace Modules.MovieTheater.Core.Entities
{
    public class Movie : IEntity<int>, IDeleted
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
