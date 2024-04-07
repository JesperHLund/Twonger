using Microsoft.AspNetCore.Mvc;
using SharedMessages;
using System.Collections.Generic;
using TweetService;

namespace TweetService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TweetController : Controller
    {
        private readonly Database.Database.TweetContext _tweetContext;
        private readonly MessageClient _messageClient;

        //Constructor
        public TweetController(Database.Database.TweetContext tweetContext, MessageClient messageClient)
        {
            _tweetContext = tweetContext;
            _messageClient = messageClient;
        }

        // GET: /{userID}/{tweetID}
        [HttpGet("{userID}/{tweetID}")]
        public ActionResult<IEnumerable<Tweet>> GetTweets(int userID, int tweetID)
        {
            List<Tweet> tweets = _tweetContext.GetNext100Tweets(userID, tweetID);

            if (tweets == null || tweets.Count == 0)
            {
                return NotFound("No more tweets"); // Return 404 Not Found if no tweets are found
            }

            return tweets;
        }

        // POST: /posttweet
        [HttpPost("postTweet")]
        public IActionResult PostTweet([FromBody] SharedMessages.Tweet tweet)
        {
            try
            {
                int tweetId = _tweetContext.AddTweet(tweet);

                if (tweetId != -1)
                {
                    tweet.Id = tweetId;

                    _messageClient.Send(
                        new TweetMessage { tweet = tweet },
                        "New Tweet"
                    );

                    return Ok(true);
                }
                else
                {
                    return BadRequest(false);
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
