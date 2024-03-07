using System.Runtime.CompilerServices;

namespace TweetService
{
    public class TweetPoster
    {
        private readonly Database.Database _database;

        public TweetPoster(Database.Database database)
        {
            _database = database;
        }

        public bool PostTweet(Tweet tweet)
        {
            if (_database.AddTweet(tweet) != -1)
            {
                tweet.Id = _database.AddTweet(tweet);

                // Tweet added successfully, implement logic to send tweet to ProfileService
                //rabbitmq stuff? I don't fucking know

                return true;
            }
            else
            {
                // Tweet was not added
                return false;
            }
        }
    }
}
