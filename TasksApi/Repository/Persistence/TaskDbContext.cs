using Microsoft.EntityFrameworkCore;
using TasksApi.Repository.Models;

namespace TasksApi.Repository.Persistence
{
    /// <summary>
    /// TaskDb Context
    /// </summary>
    public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
    {
        // Registered DB Model in TaskDbContext file
        public DbSet<TaskItem> Tasks { get; set; }

        // Added for avoid concurrency
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .Property(x => x.Version)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
