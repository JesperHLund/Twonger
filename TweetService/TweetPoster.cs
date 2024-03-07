using System.Runtime.CompilerServices;

namespace TweetService
{
    public class TweetPoster
    {
        private Database.Database.TweetContext Database;
        public bool PostTweet(Tweet tweet)
        {
            if (Database.AddTweet(tweet) != -1)
            {
                //return false if tweet is not added
                return false;
            }
            else 
            {
                //code something to send tweet to ProfileService if tweet is added
                //implement messaging to ProfileService


                //return true if tweet is added
                return true;    
            }
        }
    }
}
