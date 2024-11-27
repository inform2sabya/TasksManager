using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TasksApi.Repository.Models;
using TasksApi.Services;

namespace TasksApi.Controllers
{
    /// <summary>
    /// Controller for Tasks API
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TasksController : ControllerBase
    {
        // Local variables
        private readonly ITasksService _tasksService;
        private readonly ILogger<TasksController> _logger;
        private IHubContext<TaskHub, ITaskHubClient> _taskHub;

        /// <summary>
        /// The constructor for this class
        /// </summary>
        public TasksController(ITasksService tasksService, IHubContext<TaskHub, ITaskHubClient> taskHub, ILogger<TasksController> logger)
        {
            _tasksService = tasksService;
            _taskHub = taskHub;
            _logger = logger;
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                return Ok(await _tasksService.GetTasks());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get task by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                var task = await _tasksService.GetTask(id);
                if (task == null) return NotFound(new
                {
                    message = "Task not found!",
                    id
                });

                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Create task
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem taskItem)
        {
            try
            {
                var task = await _tasksService.CreateTask(taskItem);

                if (task == null) return BadRequest(new
                {
                    message = "Task not created, please check & try!",
                    id = task!.Id
                });

                // Inject the IHubContext to enable sending task
                await _taskHub.Clients.All.SendTask(task);

                return Ok(new
                {
                    message = "Task created successfully!",
                    id = task!.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Update task
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem taskItem)
        {
            try
            {
                var task = await _tasksService.UpdateTask(id, taskItem);
                if (task == null) return NotFound(new
                {
                    message = "Task not found!",
                    id = task!.Id
                });

                // Inject the IHubContext to enable sending task
                await _taskHub.Clients.All.SendTask(task);

                return Ok(new
                {
                    message = "Task updated successfully!",
                    id = task!.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Delete task by Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var task = await _tasksService.GetTask(id);
                if (task == null) return NotFound(new
                {
                    message = "Task not found!",
                    id
                });

                if (!await _tasksService.DeleteTask(id)) return NotFound(new
                {
                    message = "Task not found!",
                    id
                });

                // Inject the IHubContext to enable sending task
                await _taskHub.Clients.All.SendTask(task);

                return Ok(new
                {
                    message = "Task deleted successfully!",
                    id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get tasks by category
        /// </summary>
        [HttpGet("Filter")]
        public async Task<IActionResult> GetTasks([FromQuery] string category)
        {
            try
            {
                return Ok(await _tasksService.GetTasks(category));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
