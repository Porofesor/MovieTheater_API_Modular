using DataAccess.EFCore.Extends;

namespace Modules.MovieTheater.Core.Entities
{
    public class Ticket : IEntity<int>
    {
        public int Id { get; set; }
    }
}
