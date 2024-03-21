using EasyNetQ;
using SharedMessages;
using static System.Net.Mime.MediaTypeNames;

namespace TweetService
{
    public class MessageHandler:BackgroundService
    {
        private readonly MessageClient _messageClient;

        private readonly Database.Database _database;

        public void HandleProfileMessage()
        {
            

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


        }
    }
}
