using System;
using System.Collections.Generic;
using System.Linq;
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

        public Card GetCardById(string id)
        {
            foreach (var card in _battlefield.Where(card => card.Id == id))
                return card;
            return _hand.FirstOrDefault(card => card.Id == id);
        }

        private bool EnoughCost(Card card)
        {
            return this[PlayerStatsType.Metal] >= card[CardStatsType.Metal] &&
                   this[PlayerStatsType.Crystal] >= card[CardStatsType.Crystal] &&
                   this[PlayerStatsType.Deuterium] >= card[CardStatsType.Deuterium];
        }

        public bool Play(Card card)
        {
            if (!EnoughCost(card)) return false;
            this[PlayerStatsType.Metal] -= card[CardStatsType.Metal];
            this[PlayerStatsType.Crystal] -= card[CardStatsType.Crystal];
            this[PlayerStatsType.Deuterium] -= card[CardStatsType.Deuterium];
            _hand.Remove(card);
            if (card.Type == CardType.Unit)
            {
                _battlefield.Add(card);
                card.Zone = ZoneType.BattleField;
                return true;
            }
            return false;
        }

        public IEnumerable<string> GetAttackUnit()
        {
            return from unit in _battlefield where unit.CanAttack() select unit.Id;
        }
    }
}