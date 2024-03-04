﻿namespace TweetService
{
    public class Tweet
    {
        //write ma tweet class containing id and message

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