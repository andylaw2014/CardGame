using Assets.Scripts.Core.Phase;
using Assets.Scripts.Infrastructure.EventAggregator;
using Assets.Scripts.Infrastructure.IdFactory;

namespace Assets.Scripts.Core
{
    public class Game
    {
        private readonly PlayerType _first;
        private readonly GameController _gameController;
        private readonly IIdFactory _idFactory;
        private readonly EventAggregator _publisher;
        private BasePhase _phase;

        /// <summary>
        ///     Constructor of Game.
        /// </summary>
        /// <param name="gameController"></param>
        /// <param name="first">The first Player</param>
        public Game(GameController gameController, PlayerType first)
        {
            _publisher = new EventAggregator();
            _gameController = gameController;
            _first = first;
            _idFactory = new CardIdFactory();
        }

        /// <summary>
        ///     Publish an in-game message.
        /// </summary>
        /// <param name="message">Message to publish.</param>
        public void Publish(object message)
        {
            _publisher.Publish(message);
        }

        /// <summary>
        ///     Subscribe to in-game message(s).
        ///     Subscriber should implement IHandle&lt;T&gt;
        /// </summary>
        /// <param name="subscriber"></param>
        public void Subscribe(object subscriber)
        {
            _publisher.Subscribe(subscriber);
        }

        /// <summary>
        ///     Set the game phase.
        /// </summary>
        /// <param name="phase"></param>
        public void SetPhase(BasePhase phase)
        {
            _phase = phase;
            _phase.Start();
        }

        /// <summary>
        ///     Return unique card id.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetCardId(PlayerType type)
        {
            return _idFactory.GetId(_first == type ? CardIdFactory.FirstPlayer : CardIdFactory.SecondPlayer);
        }
    }
}