using TasksApi.Repository.Models;

namespace TasksApi.Services
{
    /// <summary>
    /// Interface for the TaskHub Client
    /// </summary>
    public interface ITaskHubClient
    {
        /// <summary>
        /// Broadcast updates when tasks are created/updated/deleted
        /// </summary>
        Task SendTask(TaskItem updatedTask);
    }
}
