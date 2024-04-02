using Microsoft.AspNetCore.Mvc;
using SharedMessages;
using TweetService;

namespace TweetService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : Controller
    {

        private readonly Database.Database _database;

        private readonly MessageClient _messageClient;

        //Constructor
        public TweetController(Database.Database database, MessageClient messageClient) {
            _database = database;
            _messageClient = messageClient;
        }

        // GET: api/tweet/userID/tweetID
        [HttpGet("{userID}/{tweetID}")]
        public ActionResult<IEnumerable<Tweet>> GetTweets(int userID, int tweetID)
        {
            List<Tweet> tweets = _database.GetNext100Tweets(userID, tweetID);

            if (tweets == null || tweets.Count == 0)
            {
                return NotFound("No more tweets"); // Return 404 Not Found if no tweets are found
            }

            return tweets;
        }

        // PostTweet method that takes a tweet object and returns a boolean
        // POST: api/tweet/PostTweet
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
