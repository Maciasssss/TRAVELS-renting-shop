using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRAVELS.Data;
using TRAVELS.IRepositoryInterface;
using TRAVELS.Models;

namespace TRAVELS.Models
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly TravelDBcontext _context;

        public ReservationsRepository(TravelDBcontext context)
        {
            _context = context;
        }

        public async Task<Reservation> GetReservationByIdAsync(int reservationId)
        {
            return await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservationId);
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                                 .Include(r => r.Travel)
                                 .Include(r => r.User)
                                 .ToListAsync();
        }



        public IQueryable<Reservation> GetReservationsForUser(int userId)
        {
            return (IQueryable<Reservation>)_context.Users.Where(r => r.UserId == userId);
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
          
             _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
          
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservationToDelete = await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservationId);
            if (reservationToDelete != null)
            {
                _context.Reservations.Remove(reservationToDelete);
                await _context.SaveChangesAsync();
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
