using PrimerAPI.Data;
using PrimerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PrimerAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly PrimerApiDbContext _context;

        public TaskService (PrimerApiDbContext context)
        {
            _context = context;
        }
        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .ToListAsync();
        }
        
    }
}
