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
                            new Tweet(102,"Excited to delve deeper into the world of AI! #AI #MachineLearning", 1),
                            new Tweet(109,"Just attended an amazing tech conference. So much to learn! #TechEnthusiast", 1)
                        }
                    },
                    new Profile
                    {
                        Bio = "Lover of literature and poetry. Dreamer and aspiring writer.",
                        Username = "bookworm89",
                        DisplayName = "Jane Smith",
                        Twongs = new List<Tweet>
                        {
                            new Tweet(104,"Lost in the world of books again. #BookWorm", 2),
                            new Tweet(112,"There's magic in the pages of a good book. #BookLover", 2)
                        }
                    },
                    new Profile
                    {
                        Bio = "Fitness freak, marathon runner, and health enthusiast.",
                        Username = "fitrunner77",
                        DisplayName = "David Johnson",
                        Twongs = new List<Tweet>
                        {
                            new Tweet(106,"Ran my personal best today! #FitnessGoals", 3),
                            new Tweet(113,"Healthy body, healthy mind. #FitnessMotivation", 3)
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
