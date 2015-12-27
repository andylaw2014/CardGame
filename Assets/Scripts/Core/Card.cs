using Assets.Scripts.Core.Message;
using Assets.Scripts.Core.Statistics;

namespace Assets.Scripts.Core
{
    public class Card
    {
        private readonly Game _game;
        private readonly CardStats _stats;
        public readonly string Id;
        private PlayerType _parent;
        private ZoneType _zone;

        public Card(Game game, string id, CardStats stats)
        {
            _game = game;
            _stats = stats;
            Id = id;
        }

        public ZoneType Zone
        {
            get { return _zone; }
            set
            {
                if (_zone == value) return;
                _zone = value;
                _game.Publish(new CardZoneChangeMessage(this));
            }
        }

        public PlayerType Parent
        {
            get { return _parent; }
            set
            {
                if (_parent == value) return;
                _parent = value;
                _game.Publish(new CardParentChangeMessage(this));
            }
        }

        public int this[CardStatsType statsType]
        {
            get { return _stats.Get(statsType); }
            set { _stats.Set(statsType, value); }
        }
    }
}