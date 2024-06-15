using TRAVELS.Models;

namespace TRAVELS.Iservices
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int userId);
        Task InsertAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
