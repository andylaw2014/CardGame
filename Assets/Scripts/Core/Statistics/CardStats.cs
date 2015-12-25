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