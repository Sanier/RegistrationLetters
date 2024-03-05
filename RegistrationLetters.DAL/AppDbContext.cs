using Microsoft.EntityFrameworkCore;
using RegistrationLetters.DAL.Configuration;
using RegistrationLetters.Domain.Entities;

namespace RegistrationLetters.DAL
{
    /// <summary>
    /// The AppDbContext class inherits from the DbContext class and represents the session with the database.
    /// It allows querying and saving instances of your entities.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MailEntity> Mail { get; set; }
        public DbSet<EmployeeEntity> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new MailConfiguration());
        }
    }
}
