using System.Collections.Generic;
using Assets.Scripts.Infrastructure.EventAggregator;
using Assets.Scripts.Outdate.Core.Phase;
using Assets.Scripts.Outdate.Infrastructure;
using Assets.Scripts.Outdate.UI.Command;

namespace Assets.Scripts.Outdate.Core
{
    public class Game
    {
        public enum User
        {
            You,
            Opponent
        }

        private readonly Dictionary<string, Card> _cardCache;

        private readonly User _firstUser;
        private readonly IdFactory _idFactory;
        private readonly Player _opponent;
        private readonly Player _player;
        private readonly EventAggregator _publisher;
        public readonly GameController GameController;
        private GamePhase _phase;

        public Game(GameController gameController, User firstUser)
        {
            // Initialization
            _publisher = new EventAggregator();
            GameController = gameController;
            _firstUser = firstUser;
            Log.Verbose("Is First Player:" + _firstUser);
            _player = new Player(this, User.You);
            _opponent = new Player(this, User.Opponent);
            _idFactory = new IdFactory(_firstUser == User.You);
            _cardCache = new Dictionary<string, Card>();
        }

        public void NextPhase()
        {
            _phase.Next();
        }

        public bool Handle(IUiCommand command)
        {
            Log.Verbose("Handle UI Command");
            return _phase.Handle(command);
        }

        public Card GetCardById(string id)
        {
            Card card;
            return _cardCache.TryGetValue(id, out card) ? card : null;
        }

        public void PlayCardFromHand(string id)
        {
            Card card;
            if (!_cardCache.TryGetValue(id, out card))
                return;
            card.Owner.PlayCard(card);
        }

        // TODO: Draw Card From Deck
        public void DrawCardFromDeck()
        {
            Log.Verbose("Draw Test Card From Deck");
            GameController.RpcDrawCard("TestCard", true);
        }

        public void DrawCard(string cardName, string id, User user)
        {
            var player = GetPlayer(user);
            var card = new Card(player, cardName, id);
            Log.Verbose("Add to card cache: " + id);
            _cardCache.Add(id, card);
            player.DrawCard(card);
        }

        public string GenerateId()
        {
            return _idFactory.Generate();
        }

        public void Start()
        {
            Log.Verbose("Game Start");
            _phase = new ResetPhase(this, _firstUser);
            Update();
            DrawCardFromDeck();
        }

        public void Update()
        {
            _opponent.Update();
            _player.Update();
        }

        public void SetPhase(GamePhase phase)
        {
            Log.Verbose("Change Phase:" + _phase + " => " + phase);
            _phase = phase;
        }

        public Player GetPlayer(User user)
        {
            return user == User.You ? _player : _opponent;
        }

        public Player GetOpponent(User user)
        {
            return user == User.You ? GetPlayer(User.Opponent) : GetPlayer(User.You);
        }

        public User OpponentUser(User user)
        {
            return user == User.You ? User.Opponent : User.You;
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