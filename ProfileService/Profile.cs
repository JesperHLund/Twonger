using SharedMessages;
using System.ComponentModel.DataAnnotations;

namespace ProfileService
{
    public class Profile
    {
        [Key]
        public int UserId { get; set; }
        public string Bio { get; set; }
        public string Username { get; set; }
        public string DisplayName {get; set;}

        public List<Tweet> Twongs { get; set; } = new List<Tweet>();

        
        // Parameterless constructor for Entity Framework
        public Profile()
        {
        }
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