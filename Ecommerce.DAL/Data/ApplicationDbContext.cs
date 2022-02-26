using Ecommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = Guid.NewGuid(),
                UserName = "admin",
                Password = "Admin@123",
                UserType = "admin",
                CanLogin = true,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            });
        }
    }
}
