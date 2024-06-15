using System.Threading.Tasks;
using TRAVELS.Models;
using System.Linq;

namespace TRAVELS.IRepositoryInterface
{
    public interface IGuideRepository
    {
        Task<IQueryable<Guide>> GetAllAsync();
        Task<Guide> GetByIdAsync(int GuideId);
        Task InsertAsync(Guide guide);
        Task UpdateAsync(Guide guide);
        Task DeleteAsync(int GuideId);
        Task SaveAsync();
    }
}
