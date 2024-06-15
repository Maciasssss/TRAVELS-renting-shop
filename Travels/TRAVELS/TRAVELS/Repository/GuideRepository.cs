using TRAVELS.Data;
using TRAVELS.IRepositoryInterface;
using TRAVELS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TRAVELS.Repository
{
    public class GuideRepository : IGuideRepository
    {
        private readonly TravelDBcontext _context;

        public GuideRepository(TravelDBcontext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Guide>> GetAllAsync()
        {
            var list = await _context.Guides.ToListAsync();
            return list.AsQueryable();
        }

        public async Task<Guide> GetByIdAsync(int GuideId)
        {
            return await _context.Guides.FirstOrDefaultAsync(g => g.GuideId == GuideId);
        }

        public async Task InsertAsync(Guide guide)
        {
            if (guide == null)
            {
                throw new ArgumentNullException(nameof(guide));
            }

            _context.Guides.Add(guide);
            await SaveAsync();
        }

        public async Task UpdateAsync(Guide guide)
        {
            if (guide == null)
            {
                throw new ArgumentNullException(nameof(guide));
            }

            _context.Guides.Update(guide);
            await SaveAsync();
        }

        public async Task DeleteAsync(int GuideId)
        {
            var guideToDelete = await _context.Guides.FirstOrDefaultAsync(g => g.GuideId == GuideId);
            if (guideToDelete != null)
            {
                _context.Guides.Remove(guideToDelete);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
