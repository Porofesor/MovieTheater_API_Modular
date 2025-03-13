using DataAccess.EFCore.UnitOfWork;
using Modules.MovieTheater.Core.Abstractions;
using Modules.MovieTheater.Core.Abstractions.Repositories;
using Modules.MovieTheater.Core.Entities;
using Modules.MovieTheater.Infrastructure.Persistance;
using Modules.MovieTheater.Infrastructure.Repositories;

namespace Modules.MovieTheater.Infrastructure.UnitOfWork
{
    public class MovieTheaterUnitOfWork : UnitOfWork<MovieTheaterDbContext>, IMovieTheaterUnitOfWork, IDisposable
    {
        private readonly MovieTheaterDbContext _context;
        public MovieTheaterUnitOfWork(MovieTheaterDbContext context): base(context)
        {
            _context = context;
        }
        private ICinemaRepository cinemaRepository {  get; set; }
        public ICinemaRepository CinemaRepository => cinemaRepository ??= new CinemaRepository(_context);

        private ICinemaRoomRepository cinemaRoomRepository { get; set; }
        public ICinemaRoomRepository CinemaRoomRepository => cinemaRoomRepository ??= new CinemaRoomRepository(_context);
    
        private ILocationRepository locationRepository { get; set; }
        public ILocationRepository LocationRepository => locationRepository ??= new LocationRepository(_context);

        private IMovieRepository movieRepository { get; set; }
        public IMovieRepository MovieRepository => movieRepository ??= new MovieRepository(_context);

        private IPaymentRepository paymentRepository { get; set; }
        public IPaymentRepository PaymentRepository => paymentRepository ??= new PaymentRepository(_context);

        private IPaymentTypeRepository paymentTypeRepository { get; set; }
        public IPaymentTypeRepository PaymentTypeRepository => paymentTypeRepository ??= new PaymentTypeRepository(_context);

        private IReservationRepository reservationRepository { get; set; }
        public IReservationRepository ReservationRepository => reservationRepository ??= new ReservationRepository(_context);

        private IScreeningRepository screeningRepository { get; set; }
        public IScreeningRepository ScreeningRepository => screeningRepository ??= new ScreeningRepository(_context);

        private ISeatRepository seatRepository { get; set; }
        public ISeatRepository SeatRepository => seatRepository ??= new SeatRepository(_context);   

        private ITicketRepository ticketRepository { get; set; }
        public ITicketRepository TicketRepository => ticketRepository ??= new TicketRepository(_context);   

        private IUserRepository userRepository { get; set;}
        public IUserRepository UserRepository => userRepository ??= new UserRepository(_context);   

    }

}
