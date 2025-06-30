using Microsoft.EntityFrameworkCore;
using otardavitashvili.Models;

namespace otardavitashvili.DB
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Car> Cars { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply entity configurations
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfiguration());
        }
    }
}
