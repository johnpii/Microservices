using Auth.Utilities;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Helpers;
using SharedLibrary.Models;

namespace Auth.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(y => y.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(y => y.Email).IsRequired();
            modelBuilder.Entity<User>().Property(y => y.Email).HasMaxLength(30);
            modelBuilder.Entity<User>().Property(y => y.Username).IsRequired();
            modelBuilder.Entity<User>().Property(y => y.Username).HasMaxLength(30);
            modelBuilder.Entity<User>().Property(y => y.Password).IsRequired();
            modelBuilder.Entity<User>().Property(y => y.Password).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(y => y.Role).HasMaxLength(10);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = ConfigurationHelper.config.GetSection("AdminDefaultEmail").Value,
                    Username = "Admin",
                    Password = PasswordEncryption.EncryptPassword(ConfigurationHelper.config.GetSection("AdminDefaultPassword").Value),
                    Role = "admin"
                }
            );
        }
    }
}
