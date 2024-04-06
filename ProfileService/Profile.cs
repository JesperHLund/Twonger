using SharedMessages;

namespace ProfileService
{
    public class Profile
    {
        public int UserId { get; set; }
        public string Bio { get; set; }
        public string Username { get; set; }
        public string DisplayName {get; set;}

        public List<Tweet> Twongs { get; set; } = new List<Tweet>();

        public Profile(int userId, string bio, string username, string displayName, List<Tweet> twongs)
        {
            UserId = userId;
            Bio = bio;
            Username = username;
            DisplayName = displayName;
            Twongs = twongs;
        }

    }
}