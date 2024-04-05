using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ProfileService.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly List<Profile> _profiles;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
            _profiles = new List<Profile>();
        }

        [HttpPost]
        public IActionResult CreateProfile(Profile profile){
            try
            {
                // Check if the profile already exists in the database
                var existingProfile = _profiles.Find(p => p.UserId == profile.UserId);
                if (existingProfile != null)
                {
                    return Conflict();
                }

                // Add the profile to the database
                _profiles.Add(profile);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the profile.");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult UpdateProfile(Profile profile)
        {
            try
            {
                // Check if the profile exists in the database
                var existingProfile = _profiles.Find(p => p.UserId == profile.UserId);
                if (existingProfile == null)
                {
                    return NotFound();
                }

                // Update the profile
                existingProfile.Bio = profile.Bio;
                existingProfile.Username = profile.Username;

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the profile.");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult GetProfile(int userId){
            try
            {
                // Check if the profile exists in the database
                var profile = _profiles.Find(p => p.UserId == userId);
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
