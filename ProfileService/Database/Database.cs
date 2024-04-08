using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using SharedMessages;

namespace ProfileService.Database
{
    public class Database
    {
        public class ProfileContext : DbContext
        {
            public ProfileContext(DbContextOptions<Database.ProfileContext> options) : base(options) {
                // Seed the database if it's empty
                if (!Profiles.Any())
                {
                    SeedData();
                }
            }
            public DbSet<Profile> Profiles { get; set; }

            private int profileId = 0;

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("Database");
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

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

            public void AddTweetToUser(int userId, Tweet tweet)
            {
                Console.WriteLine("Adding tweet to user with id: " + userId);
                var user = Profiles.FirstOrDefault(p => p.UserId == userId);
                Console.WriteLine("User found: " + user);

                if (user != null)
                {
                    // Check if the tweet already exists in the database
                    var existingTweet = user.Twongs.FirstOrDefault(t => t.Id == tweet.Id);
                    if (existingTweet != null)
                    {
                        // If the tweet already exists, attach it to the Entity Framework context
                        this.Entry(existingTweet).State = EntityState.Unchanged;
                    }
                    else
                    {
                        // If the tweet doesn't exist, add it to the Twongs list
                        user.Twongs.Add(tweet);
                    }

                    SaveChanges();
                }
                else
                {
                    // Handle case where user with the given userId doesn't exist
                    Console.WriteLine("User not found.");
                }
            }
        }
    }
}
