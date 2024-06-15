using TRAVELS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TRAVELS.Iservices
{
    public interface IGuideService
    {
        Task<IQueryable<Guide>> GetAllAsync();
        Task<Guide> GetByIdAsync(int guideId);
        Task InsertAsync(Guide guide);
        Task UpdateAsync(Guide guide);
        Task DeleteAsync(int guideId);
    }
}
