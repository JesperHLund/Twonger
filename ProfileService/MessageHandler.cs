using EasyNetQ;
using SharedMessages;
using static System.Net.Mime.MediaTypeNames;

namespace ProfileService
{
    public class MessageHandler : BackgroundService
    {
        private readonly MessageClient _messageClient;

        private readonly Database.Database _database;

        public void HandleTweetMessage()
        {


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


        }
    }
}
