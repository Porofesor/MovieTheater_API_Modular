using DataAccess.EFCore.Extends;

namespace Modules.MovieTheater.Core.Entities
{
    public class Location : IEntity<int>, IDeleted
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Addres {  get; set; }
        public string PostCode { get; set; }
        public string Streat {  get; set; }
        public string StreatNumber { get; set; }

    }
}
