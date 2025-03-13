using DataAccess.EFCore.Extends;

namespace Modules.MovieTheater.Core.Entities
{
    public class Screening : IEntity<int>, IDeleted 
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ScreeningDate { get; set; }

    }
}
