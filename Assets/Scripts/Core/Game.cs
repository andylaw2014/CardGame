using Infrastructure.EventAggregator;

namespace Core
{
    public class Game
    {
        public readonly GameController GameController;
        private readonly EventAggregator _publisher;

        public Game(GameController gameController)
        {
            GameController = gameController;
            _publisher = new EventAggregator();
        }

        public void Publish(object message)
        {
            _publisher.Publish(message);
        }

        public void Subscribe(object subscriber)
        {
            _publisher.Subscribe(subscriber);
        }
    }
}
