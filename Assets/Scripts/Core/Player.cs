using System.Collections.Generic;
using Assets.Scripts.Core.Message;
using Assets.Scripts.Core.Statistics;

namespace Assets.Scripts.Core
{
    public class Player
    {
        private readonly List<Card> _battlefield;
        private readonly Game _game;
        private readonly List<Card> _hand;
        private readonly PlayerStats _stats;
        public readonly PlayerType Type;

        public Player(Game game, PlayerType type)
        {
            _game = game;
            Type = type;
            _stats = new PlayerStats();
            _battlefield = new List<Card>();
            _hand = new List<Card>();
        }

        /// <summary>
        /// It return a deep copy of PlayerStats.
        /// </summary>
        /// <returns></returns>
        public PlayerStats GePlayerStats()
        {
            return new PlayerStats(_stats);
        }

        public int this[PlayerStatsType statsType]
        {
            get { return _stats.Get(statsType); }
            set
            {
                if (this[statsType] == value) return;
                _stats.Set(statsType, value);
                _game.Publish(new PlayerStatsChangeMessage(this));
            }
        }
    }
}