
using EasyNetQ;

namespace TweetService
{
    public class MessageClient
    {
        // The IBus allows us to listen for and send messages with RabbitMQ
        private readonly IBus _bus;

        public MessageClient(IBus bus)
        {
            _bus = bus;
        }

        public void Send<T>(T tweet, string topic) 
        {
            _bus.PubSub.Publish(tweet, topic);
        }

        public void Listen<T>(Action<T> handler, string topic)
        {
            _bus.PubSub.Subscribe(topic, handler);
        }


    }
}
