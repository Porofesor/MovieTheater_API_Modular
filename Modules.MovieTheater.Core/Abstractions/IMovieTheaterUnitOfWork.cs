using Modules.MovieTheater.Core.Abstractions.Repositories;

namespace Modules.MovieTheater.Core.Abstractions
{
    public interface IMovieTheaterUnitOfWork
    {
        ICinemaRepository CinemaRepository { get; }
        ICinemaRoomRepository CinemaRoomRepository { get; }
        ILocationRepository LocationRepository { get; }
        IMovieRepository MovieRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IPaymentTypeRepository PaymentTypeRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IScreeningRepository ScreeningRepository { get; }
        ISeatRepository SeatRepository { get; }
        ITicketRepository TicketRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}
