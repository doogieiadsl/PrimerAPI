using Microsoft.EntityFrameworkCore;
using PrimerAPI.Data;
using PrimerAPI.Models;

namespace PrimerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly PrimerApiDbContext _context;

        public UserService (PrimerApiDbContext context)
        {
            _context = context;
        }
        public async Task<User?> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return _context.Users.AsNoTracking();
        }

        public async Task<User?> GetByIdAsync(int Id)
        {
            return _context.Users.FirstOrDefault(p=> p.Id == Id);
        }
    }
}
