using DataAccess.EFCore.Extends;

namespace Modules.MovieTheater.Core.Entities
{
    public class Seat : IEntity<int>
    {
        public int Id { get; set; }
    }
}
