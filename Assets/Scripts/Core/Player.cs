using System;
using System.Collections.Generic;
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

        public int this[PlayerStatsType statsType]
        {
            get { return _stats.Get(statsType); }
            set
            {
                if (this[statsType] == value) return;
                _stats.Set(statsType, value);
            }
        }

        public void Add(ZoneType zone, Card card)
        {
            switch (zone)
            {
                case ZoneType.Hand:
                    _hand.Add(card);
                    break;
                case ZoneType.BattleField:
                    _battlefield.Add(card);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(zone.ToString(), zone, null);
            }
        }

        /// <summary>
        ///     It return a deep copy of PlayerStats.
        /// </summary>
        /// <returns></returns>
        public PlayerStats GePlayerStats()
        {
            return new PlayerStats(_stats);
        }

        public void RestoreResource()
        {
            _stats[PlayerStatsType.Metal] = _stats[PlayerStatsType.MaxMetal];
            _stats[PlayerStatsType.Crystal] = _stats[PlayerStatsType.MaxCrystal];
            _stats[PlayerStatsType.Deuterium] = _stats[PlayerStatsType.Deuterium];
        }
    }
}