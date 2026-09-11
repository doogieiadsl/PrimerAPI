using PrimerAPI.Models;

namespace PrimerAPI.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int Id);
        Task<TaskItem> CreateAsync(TaskItem task);

    }
}
