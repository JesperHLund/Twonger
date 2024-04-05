﻿namespace ProfileService
{
    public class Profile
    {
        public int UserId { get; set; }
        public string Bio { get; set; }
        public string Username { get; set; }
        public string DisplayName {get; set;}

        public Dictionary<int, string> Twongs { get; set; } = new Dictionary<int, string>(100);

        public Profile(int userId, string bio, string username, string displayName, Dictionary<int, string> twongs)
        {
            UserId = userId;
            Bio = bio;
            Username = username;
            DisplayName = displayName;
            Twongs = twongs;
        }

    }
}