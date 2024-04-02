using EasyNetQ;

namespace ProfileService
{
    public class MessageClient
    {
        private readonly IBus _bus;

        public MessageClient(IBus bus)
        {
            _bus = bus;
        }

        public void Send<T>(T tweet, string topic) 
        {
            _bus.PubSub.Publish(tweet, topic);
        }

        public void listen<T>(Action<T> handler, string topic)
        {
            _bus.PubSub.Subscribe(topic, handler);
        }


    }
}