using Microsoft.AspNetCore.SignalR;
using TasksApi.Repository.Models;

namespace TasksApi.Services
{
    public class TaskHub : Hub<ITaskHubClient>
    {
        /// <summary>
        /// Broadcast updates when tasks are created/updated/deleted
        /// </summary>
        public async Task SendUpdatedTask(TaskItem updatedTask)
        {
            await Clients.All.SendTask(updatedTask);
        }
    }
}
