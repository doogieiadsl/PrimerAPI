using PrimerAPI.Models;

namespace PrimerAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int Id);
        Task<User?> CreateAsync(User user);
    }
}
