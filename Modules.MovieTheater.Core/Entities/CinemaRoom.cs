using DataAccess.EFCore.Extends;

namespace Modules.MovieTheater.Core.Entities
{
    public class CinemaRoom : IEntity<int>, IDeleted
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int RoomNumber { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }

    }
}
