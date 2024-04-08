using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Newtonsoft.Json;
using SharedMessages;

namespace ProfileService.Database
{
    public class Database
    {
        public class ProfileContext : DbContext
        {
            public ProfileContext(DbContextOptions<Database.ProfileContext> options) : base(options)
            {
                // Seed the database if it's empty
                if (!Profiles.Any())
                {
                    SeedData();
                }
            }

            public DbSet<Profile> Profiles { get; set; }
            private readonly HttpClient _httpClient;

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


            public Profile AddProfile(Profile profile)
            {
                Profiles.Add(profile);
                SaveChanges();
                return profile;
            }

            public Profile UpdateProfile(Profile profile)
            {
                var existingProfile = Profiles.Find(profile.UserId);
                if (existingProfile != null)
                {
                    existingProfile.Username = profile.Username;
                    existingProfile.Bio = profile.Bio;
                    SaveChanges();
                }

                return existingProfile;
            }

            public Profile GetProfileById(int userId)
            {
                return Profiles.Find(userId);
            }

            public async Task GetMoreTweets(int userId, int tweetId)
            {
                var profile = Profiles.Find(userId);
                if (profile == null) return;
                var response = await _httpClient.GetAsync($"http://localhost:5271/api/tweet/{userId}/{tweetId}");
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var tweets = JsonConvert.DeserializeObject<List<Tweet>>(responseContent);
                foreach (var tweet in tweets)
                {
                    profile.Twongs.Add(tweet);
                }
            }

        }
    }
}
