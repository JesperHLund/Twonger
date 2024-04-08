using Microsoft.AspNetCore.Mvc;

namespace ProfileService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly List<Profile> _profiles;
        private readonly MessageClient _messageClient;
        private readonly UserProfileService _userProfileService;

        public ProfileController(ILogger<ProfileController> logger,
            UserProfileService userProfileService)
        {
            _logger = logger;
            _userProfileService = userProfileService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProfile(Profile profile)
        {
            try
            {
                // Check if the profile already exists in the database
                var existingProfile = _userProfileService.GetProfileById(profile.UserId);
                if (existingProfile != null)
                {
                    return Conflict();
                }

                // Add the profile to the database
                _userProfileService.AddProfile(profile);
                _userProfileService.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the profile.");
                return StatusCode(500);
            }
        }

        [HttpPost("update/{userId}")]
        public async Task<IActionResult> UpdateProfile(Profile profile)
        {
            try
            {
                // Check if the profile exists in the database
                var existingProfile = _userProfileService.GetProfileById(profile.UserId);
                if (existingProfile == null)
                {
                    return NotFound();
                }

                // Update the profile
                existingProfile.Bio = profile.Bio;
                existingProfile.Username = profile.Username;

                // Save changes to the database
                _userProfileService.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the profile.");
                return StatusCode(500);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            try
            {
                // Check if the profile exists in the database
                var profile = _userProfileService.GetProfileById(userId);
                if (profile == null)
                {
                    return NotFound();
                }

                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the profile.");
                return StatusCode(500);
            }
        }
    }
}
