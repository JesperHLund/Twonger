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
            // Database Tweet sets
            public DbSet<Tweet> Tweets { get; set; }

            // Constructor
            public TweetContext(DbContextOptions<TweetContext> options)
                : base(options)
            {
            }
        }

        // Database context
        private readonly TweetContext _context;

        // Constructor
        public Database(TweetContext context)
        {
            _context = context;
        }

        // Adds a tweet to the database
        public int AddTweet(Tweet tweet)
        {
            // Adds the tweet to the database
            _context.Tweets.Add(tweet);

            // Saves the changes to the database
            _context.SaveChanges();

            // Returns the id of the tweet if tweet is added
            return tweet.Id;
        }

        // Gets all tweets from the database for a specific user
        public List<Tweet> GetAllTweets(int userId)
        {
            return _context.Tweets.Where(tweet => tweet.UserID == userId).ToList();
        }
    }
}
