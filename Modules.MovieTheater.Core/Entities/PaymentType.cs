using DataAccess.EFCore.Extends;

namespace Modules.MovieTheater.Core.Entities
{
    public class PaymentType : IEntity<int>, IDeleted, IBlocked
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }
        public string Name { get; set; }
    }
}
