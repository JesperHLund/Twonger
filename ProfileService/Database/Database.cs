using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using SharedMessages;

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
                {
                    // Seed the database if it's empty
                    if (!Profiles.Any())
                    {
                        SeedData();
                    }
                }

            }

            private void SeedData()
            {
                var profiles = new List<Profile>
                {
                    new Profile
                    {
                        Bio = "Tech enthusiast, passionate about AI and machine learning.",
                        Username = "techlover23",
                        DisplayName = "John Doe",
                        Twongs = new List<Tweet>
                        {
                        }
                    },
                    new Profile
                    {
                        Bio = "Lover of literature and poetry. Dreamer and aspiring writer.",
                        Username = "bookworm89",
                        DisplayName = "Jane Smith",
                        Twongs = new List<Tweet>
                        {
                        }
                    },
                    new Profile
                    {
                        Bio = "Fitness freak, marathon runner, and health enthusiast.",
                        Username = "fitrunner77",
                        DisplayName = "David Johnson",
                        Twongs = new List<Tweet>
                        {
                        }
                    }
                };

                Profiles.AddRange(profiles);
                SaveChanges();
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
