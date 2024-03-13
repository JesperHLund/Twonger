using EasyNetQ;
using SharedMessages;

namespace TweetService
{
    public class MessageHandler:BackgroundService
    {
        private readonly MessageClient _messageClient;

        public void HandleProfileMessage(ProfileMessage message)
        {
            //Do something with the message

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            

            var messageClient = new MessageClient(
                RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
                );

            messageClient.listen<ProfileMessage>(HandleProfileMessage, "profile-message");


            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }   

        }
    }
}
