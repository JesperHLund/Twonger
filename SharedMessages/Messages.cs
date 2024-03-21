namespace SharedMessages
{
    //TweetMessage class
    public class TweetMessage
    {
        public Tweet tweet { get; set; }

    }

    //ProfileMessage class
    public class ProfileMessage
    {

        //This may need to be changed.
        public int userId { get; set; }
    }

    //AllTweetsMessage class
    public class AllTweetsMessage
    {
        //List of tweets
        public List<Tweet> Tweets { get; set; }
    }


}
