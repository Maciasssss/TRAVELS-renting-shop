using TRAVELS.IRepositoryInterface;
using TRAVELS.Iservices;
using TRAVELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReservationService : IReservationService
{
    private readonly IReservationsRepository _reservationsRepository;

    public ReservationService(IReservationsRepository reservationsRepository)
    {
        _reservationsRepository = reservationsRepository;
    }

    public Task<List<Reservation>> GetAllReservationsAsync()
    {
        return _reservationsRepository.GetAllReservationsAsync();
    }

    public async Task<Reservation> GetReservationByIdAsync(int reservationId)
    {
        return await _reservationsRepository.GetReservationByIdAsync(reservationId);
    }

    public IQueryable<Reservation> GetReservationsForUser(int userId)
    {
        return _reservationsRepository.GetReservationsForUser(userId);
    }

    public async Task AddReservationAsync(Reservation reservation)
    {
        await _reservationsRepository.AddReservationAsync(reservation);
    }

    public async Task UpdateReservationAsync(Reservation reservation)
    {
        await _reservationsRepository.UpdateReservationAsync(reservation);
    }

    public async Task DeleteReservationAsync(int reservationId)
    {
        await _reservationsRepository.DeleteReservationAsync(reservationId);
    }

  
    public IQueryable<Reservation> FilterReservationsByTravelId(int travelId)
    {
        return _reservationsRepository.GetReservationsForUser(travelId);
    }

    public IQueryable<Reservation> FilterReservationsByUserId(int userId)
    {
        return _reservationsRepository.GetReservationsForUser(userId);
    }

    public async Task<IQueryable<Reservation>> FilterReservationsByDateAsync(DateTime date)
    {
        var reservations = await _reservationsRepository.GetAllReservationsAsync();
        return reservations.Where(r => r.ReservationDate.Date == date.Date).AsQueryable();
    }

    public async Task<IQueryable<Reservation>> FilterReservationsByNumberOfPeopleAsync(int numberOfPeople)
    {
        var reservations = await _reservationsRepository.GetAllReservationsAsync();
        return reservations.Where(r => r.NumberOfPeople == numberOfPeople).AsQueryable();
    }

}
