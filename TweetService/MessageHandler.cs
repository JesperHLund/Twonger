using EasyNetQ;
using SharedMessages;
using static System.Net.Mime.MediaTypeNames;

namespace TweetService
{
    public class MessageHandler:BackgroundService
    {
        private readonly MessageClient _messageClient;

        private readonly Database.Database _database;

        public void HandleProfileMessage(ProfileMessage message)
        {
            // Get all tweets from the database
            List<SharedMessages.Tweet> tweets = _database.GetAllTweets();

            // Send a message containing all tweets
            _messageClient.Send(new AllTweetsMessage { Tweets = tweets }, "all-tweets-message");

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var messageClient = new MessageClient(
                RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
                );

            messageClient.listen<ProfileMessage>(HandleProfileMessage, "get-all-tweets-message");


            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }   

        }
    }
}
