using Microsoft.AspNetCore.SignalR.Protocol;
using System.Runtime.CompilerServices;
using SharedMessages;

namespace TweetService
{
    public class TweetPoster
    {
        private readonly Database.Database _database;

        private readonly MessageClient _messageClient;

        public TweetPoster(Database.Database database, MessageClient messageClient)
        {
            _database = database;
            _messageClient = messageClient;
        }

        public bool PostTweet(Tweet tweet)
        {
            if (_database.AddTweet(tweet) != -1)
            {
                tweet.Id = _database.AddTweet(tweet);
               
                // Tweet added successfully, implement logic to send tweet to ProfileService
                //rabbitmq stuff? I don't fucking know

                _messageClient.Send(
                    new TweetMessage { tweet = tweet },
                    "tweet-message"
                    );

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
