namespace TweetService
{
    public class Tweet
    {
        
        public int Id { get; set; }
        public string Body { get; set; }
        public int UserID { get; set; }

        public Tweet(int id, string body, int userID)
        {
            Id = id;
            Body = body;
            UserID = userID;
        }

    }
}
