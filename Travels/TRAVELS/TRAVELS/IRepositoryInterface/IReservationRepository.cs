using TRAVELS.Models;

namespace TRAVELS.IRepositoryInterface
{
    public interface IReservationsRepository
    {
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int reservationId);
        IQueryable<Reservation> GetReservationsForUser(int userId);
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int reservationId);
        Task SaveAsync();
    }
}
