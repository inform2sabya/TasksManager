using System.ComponentModel.DataAnnotations;

namespace TasksApi.Repository.Models
{
    /// <summary>
    /// Task Item
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Id of the task
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the task
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Description of the task
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Category of the task
        /// </summary>
        public required string Category { get; set; }

        /// <summary>
        /// Concurrency check
        /// </summary>
        [ConcurrencyCheck]
        public Guid Version { get; set; }
    }
}
