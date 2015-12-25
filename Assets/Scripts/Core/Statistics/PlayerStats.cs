using Assets.Scripts.Utility;

namespace Assets.Scripts.Core.Statistics
{
    public class PlayerStats : Statistics<PlayerStatsType>
    {
        public PlayerStats()
        {
            foreach (var type in Helper.GetValues<PlayerStatsType>())
            {
                Set(type, 0);
            }
        }
    }

    public enum PlayerStatsType
    {
        Hp,
        Metal,
        Crystal,
        Deuterium,
        MaxHp,
        MaxMetal,
        MaxCrystal,
        MaxDeuterium
    }
}