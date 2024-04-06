using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace ProfileService.Database
{
    public class Database
    {
        public class ProfileContext : DbContext
        {
            public ProfileContext(DbContextOptions<ProfileContext> options) : base(options) { }
            public DbSet<Profile> Profiles { get; set; }

            private int profileId = 0;

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("Database");
            }

            public void AddProfile(Profile profile)
            {
                profile.UserId = profileId++;
                Profiles.Add(profile);
                SaveChanges();
            }
        }
    }
}
