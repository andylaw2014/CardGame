using System;
using Assets.Scripts.Core.Event;
using Assets.Scripts.Core.Message;
using Assets.Scripts.Core.Phase;
using Assets.Scripts.Infrastructure.EventAggregator;
using Assets.Scripts.Infrastructure.IdFactory;

namespace Assets.Scripts.Core
{
    public class Game : IHandle<CardParentChangeMessage>, IHandle<CardZoneChangeMessage>,
        IHandle<PlayerStatsChangeMessage>
    {
        private readonly EventAggregator _eventAggregator;
        private readonly PlayerType _first;
        private readonly IIdFactory _idFactory;
        private BasePhase _phase;

        /// <summary>
        ///     Constructor of Game.
        /// </summary>
        /// <param name="first">The first Player</param>
        public Game(PlayerType first)
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);
            _first = first;
            _idFactory = new CardIdFactory();
        }

        public event EventHandler<PhaseChangeEventArg> OnPhaseChange = (sender, arg) => { };
        public event EventHandler<CardChangeEventArg> OnCardMove = (sender, arg) => { };
        public event EventHandler<PlayerChangeEventArg> OnPlayerStatsChange = (sender, arg) => { };

        /// <summary>
        ///     Publish an in-game message.
        /// </summary>
        /// <param name="message">Message to publish.</param>
        public void Publish(GameMessage message)
        {
            _eventAggregator.Publish(message);
        }

        /// <summary>
        ///     Subscribe to in-game message(s).
        ///     Subscriber should implement IHandle&lt;T&gt;
        /// </summary>
        /// <param name="subscriber"></param>
        public void Subscribe(object subscriber)
        {
            _eventAggregator.Subscribe(subscriber);
        }

        /// <summary>
        ///     Set the game phase.
        /// </summary>
        /// <param name="phase"></param>
        public void SetPhase(BasePhase phase)
        {
            _phase = phase;
            OnPhaseChange(this, new PhaseChangeEventArg(_phase));
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

        #region Handle

        public void Handle(CardParentChangeMessage message)
        {
            OnCardMove(this, new CardChangeEventArg(message.Card));
        }

        public void Handle(CardZoneChangeMessage message)
        {
            OnCardMove(this, new CardChangeEventArg(message.Card));
        }

        public void Handle(PlayerStatsChangeMessage message)
        {
            OnPlayerStatsChange(this, new PlayerChangeEventArg(message.Player));
        }

        #endregion
    }
}