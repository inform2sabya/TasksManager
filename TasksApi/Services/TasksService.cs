using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksApi.Repository.Models;
using TasksApi.Repository.Persistence;

namespace TasksApi.Services
{
    public class TasksService : ITasksService
    {
        // Local variables
        private readonly TaskDbContext _dbContext;

        /// <summary>
        /// The constructor for this class
        /// </summary>
        public TasksService(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        public async Task<List<TaskItem>> GetTasks()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        /// <summary>
        /// Get task by Id
        /// </summary>
        public async Task<TaskItem?> GetTask(int id)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Create task
        /// </summary>
        public async Task<TaskItem?> CreateTask(TaskItem taskItem)
        {
            _dbContext.Tasks.Add(taskItem);
            var result = await _dbContext.SaveChangesAsync();

            return result >= 0 ? taskItem : null;
        }

        /// <summary>
        /// Update task
        /// </summary>
        public async Task<TaskItem?> UpdateTask(int id, TaskItem taskItem)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(index => index.Id == id);
            // Checking task is exists or not
            if (task != null)
            {
                task.Name = taskItem.Name;
                task.Description = taskItem.Description;
                task.Category = taskItem.Category;
                task.Version = Guid.NewGuid();

                var hasSaved = false;
                while (!hasSaved)
                {
                    try
                    {
                        var result = await _dbContext.SaveChangesAsync();
                        hasSaved = true;

                        return result >= 0 ? task : null;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        throw new InvalidOperationException(ex.Message);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Delete task by Id
        /// </summary>
        public async Task<bool> DeleteTask(int id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(index => index.Id == id);
            // Checking task is exists or not
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);
                var result = await _dbContext.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }

        /// <summary>
        /// Get tasks by category
        /// </summary>
        public async Task<List<TaskItem>> GetTasks([FromQuery] string category)
        {
            return await _dbContext.Tasks.Where(x => x.Category.ToUpper() == category.ToUpper()).ToListAsync();
        }
    }
}
