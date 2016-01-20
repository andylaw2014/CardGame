using Assets.Scripts.Utility;

namespace Assets.Scripts.Core.Statistics
{
    public class CardStats : Statistics<CardStatsType>
    {
        public CardStats()
        {
            foreach (var type in Extension.GetValues<CardStatsType>())
            {
                Set(type, 0);
            }
        }

        public CardStats(Gui.Card.Statistics stats)
        {
            Set(CardStatsType.Hp, stats.Hp);
            Set(CardStatsType.Atk, stats.Atk);
            Set(CardStatsType.Metal, stats.Metal);
            Set(CardStatsType.Crystal, stats.Crystal);
            Set(CardStatsType.Deuterium, stats.Deuterium);
        }

        public Gui.Card.Statistics ToStatistics()
        {
            return new Gui.Card.Statistics()
            {
                Hp = this[CardStatsType.Hp],
                Atk = this[CardStatsType.Atk],
                Metal = this[CardStatsType.Metal],
                Crystal = this[CardStatsType.Crystal],
                Deuterium = this[CardStatsType.Deuterium]
            };
        }
    }

    public enum CardStatsType
    {
        Hp,
        Atk,
        Metal,
        Crystal,
        Deuterium
    }
}