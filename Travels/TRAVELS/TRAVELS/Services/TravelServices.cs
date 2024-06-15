using System.Collections.Generic;
using System.Threading.Tasks;
using TRAVELS.Models;
using TRAVELS.IRepositoryInterface;
using TRAVELS.Iservices;
using Microsoft.EntityFrameworkCore;

namespace TRAVELS.Services
{
    public class TravelService : ITravelService
    {
        private readonly ITravelRepository _travelRepository;

        public TravelService(ITravelRepository travelRepository)
        {
            _travelRepository = travelRepository;
        }

        public async Task<List<Travel>> GetAllAsync()
        {
            return await _travelRepository.GetAllAsync();
        }

        public async Task<Travel> GetByIdAsync(int travelId)
        {
            return await _travelRepository.GetByIdAsync(travelId);
        }

        public async Task InsertAsync(Travel travel)
        {
            await _travelRepository.InsertAsync(travel);
        }

        public async Task UpdateAsync(Travel travel)
        {
            await _travelRepository.UpdateAsync(travel);
        }

        public async Task DeleteAsync(int travelId)
        {
            await _travelRepository.DeleteAsync(travelId);
        }
        public async Task<Travel> GetByDestinationAsync(string destination)
        {
            return await _travelRepository.GetByDestinationAsync(destination);
        }
    }
}
