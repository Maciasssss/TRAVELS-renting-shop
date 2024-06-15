using TRAVELS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TRAVELS.IRepositoryInterface
{
    public interface IUsersRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int UserId);
        Task InsertAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int UserId);
        Task SaveAsync();
        Task<User> GetUserByUsernameAsync(string username);
    }
}
