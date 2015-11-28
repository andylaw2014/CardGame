using System;
using Assets.Scripts.Core.Phase;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.EventAggregator;

namespace Assets.Scripts.Core
{
    public class Game
    {
        public enum User
        {
            You,
            Opponent
        }

        private readonly User _firstUser;
        private readonly Player _opponent;
        private readonly Player _player;
        private readonly EventAggregator _publisher;
        private GamePhase _phase;
        private readonly GameController _gameController;

        public Game(GameController gameController, User firstUser)
        {
            // Initialization
            _publisher = new EventAggregator();
            _gameController = gameController;
            _firstUser = firstUser ;
            Log.Verbose("Is First Player:" + _firstUser);
            _player = new Player(this, User.You);
            _opponent = new Player(this, User.You);
        }

        public void NextPhaseButton()
        {
            _phase.Next();
        }

        public Card InitialCard(string name, Player player)
        {
            throw  new NotImplementedException();
        }

        public void Start()
        {
            Log.Verbose("Game Start");
            _phase = new ResetPhase(this,_firstUser);
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