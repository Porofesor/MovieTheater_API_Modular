using DataAccess.EFCore.Extends;

namespace Modules.MovieTheater.Core.Entities
{
    public class Reservation : IEntity<int>
    {
        public int Id { get; set; }
    }
}
