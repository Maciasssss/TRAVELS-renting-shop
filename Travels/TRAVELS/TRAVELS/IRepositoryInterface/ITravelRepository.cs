using System.Threading.Tasks;
using TRAVELS.Models;
using System.Collections.Generic;

namespace TRAVELS.IRepositoryInterface
{
    public interface ITravelRepository
    {
        Task<List<Travel>> GetAllAsync();
        Task<Travel> GetByIdAsync(int TravelId);
        Task InsertAsync(Travel travel);
        Task UpdateAsync(Travel travel);
        Task DeleteAsync(int TravelId);
        Task SaveAsync();
        Task<Travel> GetByDestinationAsync(string destination);
    }
}
