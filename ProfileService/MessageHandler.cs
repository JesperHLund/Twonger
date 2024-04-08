using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using SharedMessages;
using static System.Net.Mime.MediaTypeNames;

namespace ProfileService
{
    public class MessageHandler : BackgroundService
    {
        private readonly MessageClient _messageClient;
        private readonly UserProfileService _profileService;

        private readonly Database.Database.ProfileContext _database;
        
        public MessageHandler(UserProfileService profileService, Database.Database.ProfileContext database)
        {
            _messageClient = new MessageClient(
                RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
            );
            _profileService = profileService;
            _database = database;
        }

        public void HandleTweetMessage(TweetMessage tweetMessage)
        {
            try
            {
                Console.WriteLine("Received tweet message");
                Console.WriteLine("Tweet id: " + tweetMessage.tweet.Id + ", tweet body: " + tweetMessage.tweet.Body + ", tweet userid: " + tweetMessage.tweet.UserID);

                Profile profile = null;
                try
                {
                    profile = _profileService.GetProfileById(tweetMessage.tweet.UserID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception while getting profile: " + ex.Message);
                    return;
                }

                Console.WriteLine("Profile id: " + profile.UserId + ", profile username: " + profile.Username + ", profile bio: " + profile.Bio);
                if (profile != null)
                {
                    /*if (profile.Twongs == null)
                    {
                        Console.WriteLine("Twongs list is null. Initializing it.");
                        profile.Twongs = new List<Tweet>();
                    }*/

                    Console.WriteLine("Adding tweet to profile");
                    Console.WriteLine("Profile id: " + profile.UserId + ", profile username: " + profile.Username + ", profile bio: " + profile.Bio + "Tweets in this profile: " + string.Join(", ", profile.Twongs.Select(t => t.Body)));
                    Console.WriteLine("Profile tweet count before adding: " + profile.Twongs.Count);

                    Console.WriteLine($"Adding tweet to profile : {tweetMessage.tweet.Id}, {tweetMessage.tweet.Body},{tweetMessage.tweet.Id}");

                    profile.Twongs.Add(new Tweet{Id=tweetMessage.tweet.Id, Body=tweetMessage.tweet.Body, UserID = tweetMessage.tweet.UserID});

                    Console.WriteLine("Twongs in profile after adding: " + string.Join(", ", profile.Twongs.Select(t => t.Body)));
                    Console.WriteLine(string.Join(", ", profile.Twongs.Select(t => t.Body)));
                    Console.WriteLine("Profile tweet count after adding: " + profile.Twongs.Count);

                    //              _database.AddTweetToUser(profile.UserId, tweetMessage.tweet);
                    //           Console.WriteLine("Passed the AddTweetToUser method call");
                    // Retrieve the profile from the database again to ensure it's being tracked by the Entity Framework context
                    //           profile = _profileService.GetProfileById(tweetMessage.tweet.UserID);

                    _profileService.SaveChanges(); // Save changes to the database
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in HandleTweetMessage: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Starting ExecuteAsync method");
            var messageClient = new MessageClient(
                RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
            );
            Console.WriteLine("Listening for TweetMessage");
            messageClient.Listen<TweetMessage>(HandleTweetMessage, "New Tweet");
            Console.WriteLine("Finished setting up listener for TweetMessage");
        }
    }
}
