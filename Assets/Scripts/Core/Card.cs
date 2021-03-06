using System;
using Assets.Scripts.Core.Statistics;

namespace Assets.Scripts.Core
{
    public class Card : IEquatable<Card>
    {
        private readonly CardStats _stats;
        public readonly string Id;
        public readonly CardType Type;

        public Card(string id, Gui.Card card)
        {
            _stats = new CardStats(card.Stats);
            Type = card.Type;
            Id = id;
        }

        public ZoneType Zone { get; set; }

        public PlayerType Parent { get; set; }

        public int this[CardStatsType statsType]
        {
            get { return _stats.Get(statsType); }
            set { _stats.Set(statsType, value); }
        }

        public bool Equals(Card other)
        {
            return Id == other.Id;
        }

        public bool CanAttack()
        {
            return true;
        }

        public bool CanDefence()
        {
            return true;
        }

        public void Attack(Card card)
        {
            card.Defence();
            card[CardStatsType.Hp] -= this[CardStatsType.Atk];
            this[CardStatsType.Hp] -= card[CardStatsType.Atk];
        }

        public void Defence()
        {
        }

        public Gui.Card.Statistics GetStatistics()
        {
            return _stats.ToStatistics();
        }
    }
}