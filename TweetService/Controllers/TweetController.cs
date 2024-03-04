using Microsoft.AspNetCore.Mvc;

namespace TweetService.Controllers
{
    public class TweetController : Controller
    {
        //write me an action that posts a tweet
        [HttpPost]
        public IActionResult PostTweet([FromBody] Tweet tweet)
        {

            return Ok(200);
        }
    }
}
