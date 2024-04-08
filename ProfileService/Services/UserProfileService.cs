using System.Collections.Generic;
using System.Linq;
using ProfileService;
using ProfileService.Database;
using SharedMessages;
using Newtonsoft.Json;

public interface IProfileService
{
    Profile AddProfile(Profile profile);
    Profile UpdateProfile(Profile profile);
    Profile GetProfileById(int userId);
    void DeleteProfile(int userId);
}

public class UserProfileService : IProfileService
{
    private readonly Database.ProfileContext _context;
    private readonly HttpClient _httpClient;

    public UserProfileService(Database.ProfileContext context)
    {
        _context = context;
    }

    public Profile AddProfile(Profile profile)
    {
        return _context.AddProfile(profile);
    }

    public Profile UpdateProfile(Profile profile)
    {
        return _context.UpdateProfile(profile);
    }

    public Profile GetProfileById(int userId)
    {
        return _context.GetProfileById(userId);
    }

    public void DeleteProfile(int userId)
    {
        var profile = _context.Profiles.Find(userId);
        if (profile != null)
        {
            _context.Profiles.Remove(profile);
            _context.SaveChanges();
        }
    }

    public async Task GetMoreTweets(int userId, int tweetId)
    {
        await _context.GetMoreTweets(userId, tweetId);
    }
}