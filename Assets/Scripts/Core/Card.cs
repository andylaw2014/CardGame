using Assets.Scripts.Core.Statistics;

namespace Assets.Scripts.Core
{
    public class Card
    {
        private readonly CardStats _stats;
        public readonly string Id;

        public Card(string id, CardStats stats)
        {
            _stats = stats;
            Id = id;
        }

        public ZoneType Zone { get; set; }

        public PlayerType Parent { get; set; }

        public int this[CardStatsType statsType]
        {
            get { return _stats.Get(statsType); }
            set { _stats.Set(statsType, value); }
        }
    }
}