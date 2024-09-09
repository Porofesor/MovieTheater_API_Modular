using DataAccess.EFCore.Extends;

namespace Modules.Movies.Core.Entities
{
    public class Movie : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
