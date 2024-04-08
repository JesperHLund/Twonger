using Microsoft.EntityFrameworkCore;
using SharedMessages;
using System.Collections.Generic;
using System.Linq;

namespace TweetService.Database
{


    public class Database
    {
        public class TweetContext : DbContext
        {

            // Constructor
            public TweetContext(DbContextOptions<TweetContext> options) : base(options)
            {
                // Seed the database if it's empty
                if (!Tweets.Any())
                {
                    SeedData();
                }
            }

            // Database Tweet sets
            public DbSet<Tweet> Tweets { get; set; }



            private void SeedData()
            {
                var tweets = new List<Tweet>
            {
                    new Tweet("This is a sample tweet.", 1),
                    new Tweet("Another sample tweet here.", 2),
                    new Tweet("Yet another sample tweet.", 1),
                    new Tweet("Sample tweet number four.", 3),
                    new Tweet("Fifth sample tweet incoming.", 2),
                    new Tweet("Adding more seed data.", 1),
                    new Tweet("Yet another tweet for testing.", 2),
                    new Tweet("Hello, world!", 3),
                    new Tweet("Testing the seeding mechanism.", 1),
                    new Tweet("Yet another tweet.", 2),
                    new Tweet("Random tweet for testing purposes.", 3),
                    new Tweet("A tweet about something.", 1),
                    new Tweet("Tweeting all day long.", 2),
                    new Tweet("Tweeting is fun!", 3),
                    new Tweet("Testing tweet number 15.", 1),
                    new Tweet("Yet another tweet for testing purposes.", 2),
                    new Tweet("Hello from the tweet world.", 3),
                    new Tweet("Tweeting for the sake of tweeting.", 1),
                    new Tweet("This is tweet number 20.", 2),
                    new Tweet("Tweeting non-stop.", 3),
                    new Tweet("Testing tweet number 21.", 1),
                    new Tweet("Yet another tweet for testing purposes.", 2),
                    new Tweet("Hello from the tweet world.", 3),
                    new Tweet("Tweeting for the sake of tweeting.", 1),
                    new Tweet("This is tweet number 25.", 2),
                    new Tweet("Tweeting non-stop.", 3),
                    new Tweet("Testing tweet number 26.", 1),
                    new Tweet("Yet another tweet for testing purposes.", 2),
                    new Tweet("Hello from the tweet world.", 3),
                    new Tweet("Tweeting for the sake of tweeting.", 1),
                    new Tweet("This is tweet number 30.", 2),
                    new Tweet("Tweeting non-stop.", 3),
                    new Tweet("Testing tweet number 31.", 1),
                    new Tweet("Yet another tweet for testing purposes.", 2),
                    new Tweet("Hello from the tweet world.", 3),
                    new Tweet("Tweeting for the sake of tweeting.", 1),
                    new Tweet("This is tweet number 35.", 2),
                    new Tweet("Tweeting non-stop.", 3),
                    new Tweet("Testing tweet number 36.", 1),
                    new Tweet("Yet another tweet for testing purposes.", 2),
                    new Tweet("Hello from the tweet world.", 3),
                    new Tweet("Tweeting for the sake of tweeting.", 1),
                    new Tweet("This is tweet number 40.", 2),
                    new Tweet("Tweeting non-stop.", 3),
                    new Tweet("Testing tweet number 41.", 1),
                    new Tweet("Yet another tweet for testing purposes.", 2),
                    new Tweet("Hello from the tweet world.", 3),
                    new Tweet("Tweeting for the sake of tweeting.", 1),
                    new Tweet("This is tweet number 45.", 2),
                    new Tweet("Tweeting non-stop.", 3),
                    new Tweet("Testing tweet number 46.", 1),
                    new Tweet("Yet another tweet for testing purposes.", 2),
                    new Tweet("Hello from the tweet world.", 3),
                    new Tweet("Tweeting for the sake of tweeting.", 1),
                    new Tweet("This is tweet number 50.", 2),
                    new Tweet("Tweeting non-stop.", 3),
                    new Tweet("Tweet number 51.", 1),
                    new Tweet("Tweet number 52.", 2),
                    new Tweet("Tweet number 53.", 3),
                    new Tweet("Tweet number 54.", 1),
                    new Tweet("Tweet number 55.", 2),
                    new Tweet("Tweet number 56.", 3),
                    new Tweet("Tweet number 57.", 1),
                    new Tweet("Tweet number 58.", 2),
                    new Tweet("Tweet number 59.", 3),
                    new Tweet("Tweet number 60.", 1),
                    new Tweet("Tweet number 61.", 2),
                    new Tweet("Tweet number 62.", 3),
                    new Tweet("Tweet number 63.", 1),
                    new Tweet("Tweet number 64.", 2),
                    new Tweet("Tweet number 65.", 3),
                    new Tweet("Tweet number 66.", 1),
                    new Tweet("Tweet number 67.", 2),
                    new Tweet("Tweet number 68.", 3),
                    new Tweet("Tweet number 69.", 1),
                    new Tweet("Tweet number 70.", 2),
                    new Tweet("Tweet number 71.", 3),
                    new Tweet("Tweet number 72.", 1),
                    new Tweet("Tweet number 73.", 2),
                    new Tweet("Tweet number 74.", 3),
                    new Tweet("Tweet number 75.", 1),
                    new Tweet("Tweet number 76.", 2),
                    new Tweet("Tweet number 77.", 3),
                    new Tweet("Tweet number 78.", 1),
                    new Tweet("Tweet number 79.", 2),
                    new Tweet("Tweet number 80.", 3),
                    new Tweet("Tweet number 81.", 1),
                    new Tweet("Tweet number 82.", 2),
                    new Tweet("Tweet number 83.", 3),
                    new Tweet("Tweet number 84.", 1),
                    new Tweet("Tweet number 85.", 2),
                    new Tweet("Tweet number 86.", 3),
                    new Tweet("Tweet number 87.", 1),
                    new Tweet("Tweet number 88.", 2),
                    new Tweet("Tweet number 89.", 3),
                    new Tweet("Tweet number 90.", 1),
                    new Tweet("Tweet number 91.", 2),
                    new Tweet("Tweet number 92.", 3),
                    new Tweet("Tweet number 93.", 1),
                    new Tweet("Tweet number 94.", 2),
                    new Tweet("Tweet number 95.", 3),
                    new Tweet("Tweet number 96.", 1),
                    new Tweet("Tweet number 97.", 2),
                    new Tweet("Tweet number 98.", 3),
                    new Tweet("Tweet number 99.", 1),
                    new Tweet("Tweet number 100.", 2),
                };

                Tweets.AddRange(tweets);
                SaveChanges();
            }

            // Adds a tweet to the database
            public int AddTweet(Tweet tweet)
            {
                // Adds the tweet to the database
                Tweets.Add(tweet);

                // Saves the changes to the database
                SaveChanges();

                // Returns the id of the tweet if tweet is added
                return tweet.Id;
            }

            // Gets all tweets from the database for a specific user
            public List<Tweet> GetAllTweets(int userId)
            {
                return Tweets.Where(tweet => tweet.UserID == userId).ToList();
            }

            // Gets all tweets from the database for a specific user within a specified ID range
            public List<Tweet> GetNext100Tweets(int userId, int startId)
            {
                // Returns the next 100 tweets after the startId
                return Tweets
                    .Where(tweet => tweet.UserID == userId && tweet.Id < startId)
                    .OrderByDescending(tweet => tweet.Id) // Ensure tweets are ordered by ID in descending order (newest first)
                    .Take(100) // Take the next 100 tweets after the startId
                    .ToList(); //convert to list
            }
        }
    }
}
