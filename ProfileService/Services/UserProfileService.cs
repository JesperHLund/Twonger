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

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public Profile AddProfile(Profile profile)
    {
        _context.Profiles.Add(profile);
        _context.SaveChanges();
        return profile;
    }

    public Profile UpdateProfile(Profile profile)
    {
        var existingProfile = _context.Profiles.Find(profile.UserId);
        if (existingProfile != null)
        {
            existingProfile.Username = profile.Username;
            existingProfile.Bio = profile.Bio;
            _context.SaveChanges();
        }
        return existingProfile;
    }

    public Profile GetProfileById(int userId)
    {
        Console.WriteLine("Getting profile with id: " + userId);
        Console.WriteLine("Profiles: " + _context.Profiles.Count());
        return _context.Profiles.Find(userId);
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
        var profile = _context.Profiles.Find(userId);
        if (profile == null) return;
        var response = await _httpClient.GetAsync($"http://localhost:5271/api/tweet/{userId}/{tweetId}");
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var tweets = JsonConvert.DeserializeObject<List<Tweet>>(responseContent);
        foreach (var tweet in tweets)
        {
            profile.Twongs.Add(tweet);
        }
    }
}