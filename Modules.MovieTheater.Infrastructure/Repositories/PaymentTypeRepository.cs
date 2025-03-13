using DataAccess.EFCore.BaseRepository;
using Modules.MovieTheater.Core.Abstractions.Repositories;
using Modules.MovieTheater.Core.Entities;
using Modules.MovieTheater.Infrastructure.Persistance;

namespace Modules.MovieTheater.Infrastructure.Repositories
{
    public class PaymentTypeRepository : GenericRepository<PaymentType, MovieTheaterDbContext>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(MovieTheaterDbContext context) : base(context) { }
    }
}
