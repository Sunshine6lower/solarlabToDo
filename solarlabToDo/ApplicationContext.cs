using Microsoft.EntityFrameworkCore;

namespace solarlabToDo
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
            $"Host={DB.Host};Port={DB.Port};Database={DB.DatabaseName};Username={DB.Username};Password={DB.Password};SSL Mode=Prefer;Trust Server Certificate=true"); 
        }
    }
}