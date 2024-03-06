using System.Collections.Generic;
using System.Linq;

public interface IProfileService
{
    Profile AddProfile(Profile profile);
    Profile UpdateProfile(Profile profile);
    Profile GetProfile(int userId);
    void DeleteProfile(int userId);
}

public class ProfileService : IProfileService
{
    private readonly ProfileContext _context;

    public ProfileService(ProfileContext context)
    {
        _context = context;
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

    public Profile GetProfile(int userId)
    {
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
}