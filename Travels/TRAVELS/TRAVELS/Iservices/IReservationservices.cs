using TRAVELS.Models;

namespace TRAVELS.Iservices
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int reservationId);
        IQueryable<Reservation> GetReservationsForUser(int userId);
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int reservationId);

        IQueryable<Reservation> FilterReservationsByTravelId(int travelId);
        IQueryable<Reservation> FilterReservationsByUserId(int userId);
        Task<IQueryable<Reservation>> FilterReservationsByDateAsync(DateTime date);
        Task<IQueryable<Reservation>> FilterReservationsByNumberOfPeopleAsync(int numberOfPeople);
    }
}
