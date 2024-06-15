using TRAVELS.Models;

namespace TRAVELS.Iservices
{
        public interface ITravelService
        {
            Task<List<Travel>> GetAllAsync();
            Task<Travel> GetByIdAsync(int TravelId);
            Task InsertAsync(Travel travel);
            Task UpdateAsync(Travel travel);
            Task DeleteAsync(int TravelId);
            Task<Travel> GetByDestinationAsync(string destination);
    }
}
