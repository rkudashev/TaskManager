using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Models;

namespace TaskManager.API.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectAdmin> ProjectAdmins { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Ravil", 
                    LastName = "Kudashev", 
                    Email = "admin", 
                    Password = "qwerty123", 
                    Status = UserStatus.Admin,
                }
            );
        }
    }
}
