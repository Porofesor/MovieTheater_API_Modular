using DataAccess.EFCore.Extends;

namespace Modules.MovieTheater.Core.Entities
{
    public class Payment : IEntity<int>, IDeleted
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int Ammount {  get; set; }
    }
}
