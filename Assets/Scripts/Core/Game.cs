using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Event;
using Assets.Scripts.Core.Message;
using Assets.Scripts.Core.Phase;
using Assets.Scripts.Core.Statistics;
using Assets.Scripts.Infrastructure.EventAggregator;
using Assets.Scripts.Infrastructure.IdFactory;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Core
{
    public class Game : IHandle<CardParentChangeMessage>, IHandle<CardZoneChangeMessage>,
        IHandle<PlayerStatsChangeMessage>
    {
        private readonly EventAggregator _eventAggregator;
        private readonly PlayerType _first;
        private readonly GameController _gameController;
        private readonly IIdFactory _idFactory;
        private readonly Dictionary<PlayerType, Player> _players;
        private BasePhase _phase;
        private readonly int _maximumResource = 10;

        /// <summary>
        ///     Constructor of Game.
        /// </summary>
        /// <param name="gameController"></param>
        /// <param name="first">The first Player</param>
        public Game(GameController gameController, PlayerType first)
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);
            _first = first;
            _gameController = gameController;
            _idFactory = new CardIdFactory();
            _players = new Dictionary<PlayerType, Player>();
            foreach (var type in Extension.GetValues<PlayerType>())
                _players.Add(type, new Player(this, type));
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

        /// <summary>
        ///     Create a Card and Gui Card.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cardName"></param>
        /// <param name="zone"></param>
        /// <returns></returns>
        public Card CreateCard(PlayerType type, string cardName, ZoneType zone)
        {
            var id = GetCardId(type);
            var cardComponent = _gameController.CreateCard(cardName, id, type, zone);
            var cardStats = new CardStats(cardComponent.Stats);
            return new Card(this, id, cardStats);
        }

        /// <summary>
        ///     Get Player object.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Player GetPlayer(PlayerType type)
        {
            return _players[type];
        }

        public void AddResourceByPanel(PlayerType type)
        {
            var player = GetPlayer(type);
            var enableMetal = player[PlayerStatsType.MaxMetal] < _maximumResource;
            var enableCrystal = player[PlayerStatsType.MaxCrystal] < _maximumResource;
            var enableDeuterium = player[PlayerStatsType.MaxDeuterium] < _maximumResource;
            _gameController.EnableResourcePanel(enableMetal, enableCrystal, enableDeuterium);
        }

        public void AddResource(PlayerType pType, ResourceType rType, int value)
        {
            var player = GetPlayer(pType);
            switch (rType)
            {
                case ResourceType.Metal:
                    player[PlayerStatsType.MaxMetal] += value;
                    break;
                case ResourceType.Crystal:
                    player[PlayerStatsType.MaxCrystal] += value;
                    break;
                case ResourceType.Deuterium:
                    player[PlayerStatsType.MaxDeuterium] += value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(rType.ToString(), rType, null);
            }
        }

        /// <summary>
        ///     Start the game.
        /// </summary>
        public void Start()
        {
            SetPhase(new MainPhase(this, _first));
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