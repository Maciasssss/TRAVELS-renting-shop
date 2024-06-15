using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRAVELS.Data;
using TRAVELS.IRepositoryInterface;
using TRAVELS.Models;

namespace TRAVELS.Repository
{
    public class TravelRepository : ITravelRepository
    {
        private readonly TravelDBcontext _context;

        public TravelRepository(TravelDBcontext context)
        {
            _context = context;
        }

        public async Task<List<Travel>> GetAllAsync()
        {
            return await _context.Travels.ToListAsync();
        }

        public async Task<Travel> GetByIdAsync(int TravelId)
        {
            return await _context.Travels.FindAsync(TravelId);
        }

        public async Task InsertAsync(Travel travel)
        {
            _context.Travels.Add(travel);
            await SaveAsync();
        }

        public async Task UpdateAsync(Travel travel)
        {
            _context.Entry(travel).State = EntityState.Modified;
            await SaveAsync();
        }

        public async Task DeleteAsync(int TravelId)
        {
            var travel = await _context.Travels.FindAsync(TravelId);
            if (travel != null)
            {
                _context.Travels.Remove(travel);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task<Travel> GetByDestinationAsync(string destination)
        {
            if (string.IsNullOrWhiteSpace(destination))
                throw new ArgumentException("Destination cannot be null or whitespace.", nameof(destination));

            return await _context.Travels.FirstOrDefaultAsync(t => t.Destination.ToLower() == destination.ToLower());
        }
    }
}
