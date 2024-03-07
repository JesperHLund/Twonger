using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Collections.Generic;
using System.Linq;

namespace TweetService.Database
{
    public class Database
    {
        public class TweetContext : DbContext
        {

            //Database Tweet sets
            public DbSet<Tweet> Tweets { get; set; }

            //Database NextTweetId sets
            public DbSet<NextTweetId> NextTweetIds { get; set; }

            //Constructor
            public TweetContext(DbContextOptions<TweetContext> options)
                : base(options)
            {
            }

            //Seeds the database with the next tweet id
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<NextTweetId>().HasData(new NextTweetId { Id = 1, NextId = 0 });
            }
        }

        //Database context
        private readonly TweetContext _context;

        //Constructor
        public Database(TweetContext context)
        {
            _context = context;
        }

        //Adds a tweet to the database
        public int AddTweet(Tweet tweet)
        {
            var nextTweetId = _context.NextTweetIds.First().NextId;
            tweet.Id = nextTweetId;

            //Adds the tweet to the database
            _context.Tweets.Add(tweet);

            //Saves the changes to the database
            _context.SaveChanges();

            //Increments the next tweet id
            _context.NextTweetIds.First().NextId++;

            //Saves the changes to the database
            _context.SaveChanges(); 
            //Returns the id of the tweet if tweet is added
            return tweet.Id;
        }

        //Gets all tweets from the database
        public List<Tweet> GetAllTweets()
        {
            return _context.Tweets.ToList();
        }
    }

    //NextTweetId class
    public class NextTweetId
    {
        public int Id { get; set; }
        public int NextId { get; set; }
    }
}
