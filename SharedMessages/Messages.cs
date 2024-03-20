namespace SharedMessages
{
    
    public class TweetMessage
    {
        public Tweet tweet { get; set; }

    }

    public class ProfileMessage
    {

        //This may need to be changed.
        public string Message { get; set; }
    }

    public class AllTweetsMessage
    {
        public List<Tweet> Tweets { get; set; }
    }


}
