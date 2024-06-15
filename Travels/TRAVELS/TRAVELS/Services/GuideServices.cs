using TRAVELS.IRepositoryInterface;
using TRAVELS.Iservices;
using TRAVELS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TRAVELS.Services
{
    public class GuideService : IGuideService
    {
        private readonly IGuideRepository _guideRepository;

        public GuideService(IGuideRepository guideRepository)
        {
            _guideRepository = guideRepository;
        }

        public async Task<IQueryable<Guide>> GetAllAsync()
        {
            return await _guideRepository.GetAllAsync();
        }

        public async Task<Guide> GetByIdAsync(int guideId)
        {
            return await _guideRepository.GetByIdAsync(guideId);
        }

        public async Task InsertAsync(Guide guide)
        {
            await _guideRepository.InsertAsync(guide);
        }

        public async Task UpdateAsync(Guide guide)
        {
            await _guideRepository.UpdateAsync(guide);
        }

        public async Task DeleteAsync(int guideId)
        {
            await _guideRepository.DeleteAsync(guideId);
        }
    }
}
