using Microsoft.AspNetCore.Mvc;
using TweetService;

namespace TweetService.Controllers
{
    public class TweetController : Controller
    {
        public TweetPoster TweetPoster;

        [HttpPost]
        public IActionResult PostTweet([FromBody] Tweet tweet)
        {
            if(TweetPoster.PostTweet(tweet))
            {
                //Returns 200 if tweet is added
                return Ok(200);
            }
            else
            {
                //Returns 500 if tweet isn't added
                return BadRequest(400);
            }
        }
    }
}
