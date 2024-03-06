using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;

namespace TweetService.Database
{
    public class Database
    {
        public class TweetContext : DbContext
        {

            public DbSet<Tweet> Tweets { get; set; }

            private int nextTweetId = 0;


            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("Database");
            }

            public int AddTweet(Tweet tweet)
            {
                tweet.Id = nextTweetId++;
                using (var context = new TweetContext())
                {
                    context.Tweets.Add(tweet);
                    context.SaveChanges();
                    return tweet.Id;
                }
            }

            public static List<Tweet> GetAllTweets()
            {
                using (var context = new TweetContext())
                {
                    return context.Tweets.ToList();
                }
            }
        }

    }
}
