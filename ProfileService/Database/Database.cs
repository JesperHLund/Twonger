using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace ProfileService.Database
{
    public class Database
    {
        public class ProfileContext : DbContext
        {
            public DbSet<Profile> Profiles { get; set; }

            private int profileId = 0;

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("Database");
            }

            public void AddProfile(Profile profile)
            {
                profile.UserId = profileId++;
                using(var context = new ProfileContext())
                {
                    context.Profiles.Add(profile);
                    context.SaveChanges();
                }
            }
        }
    }
}
