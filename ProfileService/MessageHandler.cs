using EasyNetQ;
using SharedMessages;
using static System.Net.Mime.MediaTypeNames;

namespace ProfileService
{
    public class MessageHandler : BackgroundService
    {
        private readonly MessageClient _messageClient;
        private readonly UserProfileService _profileService;

        private readonly Database.Database _database;

        public void HandleTweetMessage(TweetMessage tweetMessage)
        {
            var profile = _profileService.GetProfileById(tweetMessage.tweet.UserID);
            profile.Twongs.Add(tweetMessage.tweet);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var messageClient = new MessageClient(
                RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
                );
        }
    }
}
