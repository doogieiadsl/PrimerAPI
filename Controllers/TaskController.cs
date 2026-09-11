using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimerAPI.Models;
using PrimerAPI.Services;

namespace PrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        // Get /api/tasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }
        // Get /api/tasks/ s(id)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task is null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        // Post /api/tasks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItem taskItem)
        {
            var created = await _taskService.CreateAsync(taskItem);
            return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
        }
    }
}
