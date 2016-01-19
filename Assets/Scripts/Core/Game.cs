using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Message;
using Assets.Scripts.Core.Phase;
using Assets.Scripts.Core.Statistics;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.EventAggregator;
using Assets.Scripts.Infrastructure.IdFactory;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Core
{
    public class Game
    {
        private const int MaximumResource = 10;
        private readonly EventAggregator _eventAggregator;
        private readonly PlayerType _first;
        private readonly GameController _gameController;
        private readonly IIdFactory _idFactory;
        private readonly Dictionary<PlayerType, Player> _players;
        private BasePhase _phase;

        /// <summary>
        ///     Constructor of Game.
        /// </summary>
        /// <param name="gameController"></param>
        /// <param name="first">The first Player</param>
        public Game(GameController gameController, PlayerType first)
        {
            _eventAggregator = new EventAggregator();
            _first = first;
            _gameController = gameController;
            _idFactory = new CardIdFactory();
            _players = new Dictionary<PlayerType, Player>();
            foreach (var type in Extension.GetValues<PlayerType>())
                _players.Add(type, new Player(this, type));
        }

        /// <summary>
        ///     Publish an in-game message.
        /// </summary>
        /// <param name="message">Message to publish.</param>
        private void Publish(GameMessage message)
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
        /// <param name="notify"></param>
        public void SetPhase(BasePhase phase, bool notify = true)
        {
            Log.Verbose("Set Phase: " + phase.GetName() + "(" + phase.Parent + ")");
            _phase = phase;
            if (notify)
                Publish(new PhaseStartMessage(_phase));
            _phase.Start();
        }

        /// <summary>
        ///     Return unique card id.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetCardId(PlayerType type)
        {
            return _idFactory.GetId(_first == type ? CardIdFactory.FirstPlayer : CardIdFactory.SecondPlayer);
        }

        /// <summary>
        ///     Get Player object.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Player GetPlayer(PlayerType type)
        {
            return _players[type];
        }

        private Player GetPlayerByCardId(string id)
        {
            return GetPlayer(_idFactory.GetType(id) == CardIdFactory.FirstPlayer ? _first : _first.Opposite());
        }

        public Card GetCardById(string id)
        {
            return GetPlayerByCardId(id).GetCardById(id);
        }

        public void AddResourceByPanel(PlayerType type)
        {
            var player = GetPlayer(type);
            var enableMetal = player[PlayerStatsType.MaxMetal] < MaximumResource;
            var enableCrystal = player[PlayerStatsType.MaxCrystal] < MaximumResource;
            var enableDeuterium = player[PlayerStatsType.MaxDeuterium] < MaximumResource;
            Publish(new EnableResourcePanelMessage(type, enableMetal, enableCrystal, enableDeuterium));
        }

        /// <summary>
        ///     Use GameController AddResource instead.
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="rType"></param>
        /// <param name="value"></param>
        /// <param name="restore"></param>
        /// <param name="notify"></param>
        public void AddResource(PlayerType pType, ResourceType rType, int value, bool restore = true, bool notify = true)
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
            if (restore)
                GetPlayer(pType).RestoreResource();
            if (notify)
                Publish(new PlayerStatsChangeMessage(GetPlayer(pType)));
        }

        /// <summary>
        ///     Start the game.
        /// </summary>
        public void Start()
        {
            Log.Verbose("Start Game");
            Publish(new PlayerStatsChangeMessage(GetPlayer(_first)));
            Publish(new PlayerStatsChangeMessage(GetPlayer(_first.Opposite())));
            SetPhase(new MainPhase(this, _first));
        }

        /// <summary>
        ///     Move to next phase.
        /// </summary>
        public void NextPhase(bool notify = true)
        {
            if (notify)
                Publish(new PhaseEndMessage(_phase));
            _phase.Next();
        }

        public void DrawCard(PlayerType type)
        {
            var id = GetCardId(type);
            _gameController.CreateCard("TestCard", id, type, ZoneType.Hand);
        }

        public void CreateCard(PlayerType type, ZoneType zone, string id, Gui.Card cardComponent)
        {
            Log.Verbose(string.Format("CreateCard:{0},{1},{2}", type, zone, id));
            var card = new Card(id, cardComponent) {Parent = type};
            GetPlayer(type).Add(zone, card);
        }

        public void TryPlay(string id)
        {
            Log.Verbose("TryPlay:" + id);
            var player = GetPlayerByCardId(id);
            var card = player.GetCardById(id);
            Log.Verbose(string.Format("{3} TryPlay:{0},{1},{2}", card.Id, card.Parent, card.Zone, player.Type));
            if (!_phase.AllowPlayCard() || !player.Play(card)) return;
            Publish(new PlayerStatsChangeMessage(player));
            Publish(new CardZoneChangeMessage(card));
            _gameController.PlayCard(id);
        }

        public void PlayCard(string id)
        {
            var player = GetPlayerByCardId(id);
            var card = player.GetCardById(id);
            Log.Verbose(string.Format("{3} PlayCard:{0},{1},{2}", card.Id, card.Parent, card.Zone, player.Type));
            player.Play(card);
            Publish(new PlayerStatsChangeMessage(player));
            Publish(new CardZoneChangeMessage(card));
        }
    }
}