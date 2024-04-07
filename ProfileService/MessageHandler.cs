using EasyNetQ;
using SharedMessages;
using static System.Net.Mime.MediaTypeNames;

namespace ProfileService
{
    public class MessageHandler : BackgroundService
    {
        private readonly MessageClient _messageClient;
        private readonly UserProfileService _profileService;

        private readonly Database.Database.ProfileContext _database;

        public void HandleTweetMessage(TweetMessage tweetMessage)
        {
            Console.WriteLine("Received tweet message");
            Console.WriteLine("Tweet id: " + tweetMessage.tweet.Id + ", tweet body: " + tweetMessage.tweet.Body + ", tweet userid: " + tweetMessage.tweet.UserID);

            var profile = _profileService.GetProfileById(tweetMessage.tweet.UserID);
            Console.WriteLine("Profile id: " + profile.UserId + ", profile username: " + profile.Username + ", profile bio: " + profile.Bio);
            if (profile != null)
            {

                Console.WriteLine("Adding tweet to profile");
                Console.WriteLine("Profile id: " + profile.UserId + ", profile username: " + profile.Username + ", profile bio: " + profile.Bio);
                Console.WriteLine("Profile tweet count: " + profile.Twongs.Count);
                Console.WriteLine("Tweet id: " + tweetMessage.tweet.Id + ", tweet body: " + tweetMessage.tweet.Body + ", tweet body: " + tweetMessage.tweet.Id);
                profile.Twongs.Add(tweetMessage.tweet);
                _database.AddTweetToUser(profile.UserId, tweetMessage.tweet);
                _database.SaveChanges(); // Save changes to the database
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var messageClient = new MessageClient(
                RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
                );
            messageClient.Listen<TweetMessage>(HandleTweetMessage, "New Tweet");
        }
    }
}
