using Microsoft.AspNetCore.Mvc;
using SharedMessages;
using TweetService;

namespace TweetService.Controllers
{
    public class TweetController : Controller
    {

        private readonly Database.Database _database;

        private readonly MessageClient _messageClient;

        //Constructor
        public TweetController(Database.Database database, MessageClient messageClient) {
            _database = database;
            _messageClient = messageClient;
        }

        [HttpPost]
        public bool PostTweet([FromBody] SharedMessages.Tweet tweet)
        {
            //Attempts to add tweet to database and takes the returned value and adds it to the tweetId variable
            int tweetId = _database.AddTweet(tweet);

            //if tweet id is not -1, then it was added successfully, we can send it to the message client and return true
            if (tweetId != -1)
            {
                tweet.Id = tweetId;


                _messageClient.Send(
                    new TweetMessage { tweet = tweet },
                    "tweet-message"
                    );

                return true;
            }
            //if tweet id is -1, then it was not added successfully and we return false
            else
            {
                // Tweet was not added
                return false;
            }
        }
    }
}
