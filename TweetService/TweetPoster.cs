namespace TweetService
{
    public class TweetPoster
    {
        public bool PostTweet(Tweet tweet)
        {
            if (Database.Database.TweetContext.AddTweet(tweet) != -1)
            {
                //return false if tweet is not added
                return false;
            }
            else 
            {
                //code something to send tweet to ProfileService if tweet is added

                //return true if tweet is added
                return true;    
            }
        }
    }
}
