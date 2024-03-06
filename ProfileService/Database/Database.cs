using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace ProfileService.Database
{
    public class Database
    {
        public class ProfileContext : DbContext
        {
            public DbSet<Profile> Profiles { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("Database");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Configure the Profile entity
                modelBuilder.Entity<Profile>().HasKey(p => p.UserId);
                // Add any additional configuration for the Profile entity

                // Configure any other entities and their relationships here

                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
