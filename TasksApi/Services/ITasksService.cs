using Microsoft.AspNetCore.Mvc;
using TasksApi.Repository.Models;

namespace TasksApi.Services
{
    /// <summary>
    /// Interface for the tasks service
    /// </summary>
    public interface ITasksService
    {
        /// <summary>
        /// Get all tasks
        /// </summary>
        Task<List<TaskItem>> GetTasks();

        /// <summary>
        /// Get task by Id
        /// </summary>
        Task<TaskItem?> GetTask(int id);

        /// <summary>
        /// Create task
        /// </summary>
        Task<TaskItem?> CreateTask(TaskItem taskItem);

        /// <summary>
        /// Update task
        /// </summary>
        Task<TaskItem?> UpdateTask(int id, TaskItem taskItem);

        /// <summary>
        /// Delete task by Id
        /// </summary>
        Task<bool> DeleteTask(int id);

        /// <summary>
        /// Get tasks by category
        /// </summary>
        Task<List<TaskItem>> GetTasks([FromQuery] string category);
    }
}
