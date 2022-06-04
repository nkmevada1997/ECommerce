using Ecommerce.Common.Enum;
using Ecommerce.DAL.Models;
using Ecommerce.Helper.Encode;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                Email = "admin@gmail.com",
                Password = EncodeBase.EncodeBase64("Admin@123"),
                UserName = "Admin",
                UserType = UserType.Admin,
                CanLogin = true,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
