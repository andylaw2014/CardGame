using Assets.Scripts.Utility;

namespace Assets.Scripts.Core.Statistics
{
    public class PlayerStats : Statistics<PlayerStatsType>
    {
        public PlayerStats(int hp = 30, int metal = 0, int crystal = 0, int deuterium = 0,
            int maxHp = 30, int maxMetal = 0, int maxCrystal = 0, int maxDeuterium = 0)
        {
            Set(PlayerStatsType.Hp, hp);
            Set(PlayerStatsType.Metal, metal);
            Set(PlayerStatsType.Crystal, crystal);
            Set(PlayerStatsType.Deuterium, deuterium);
            Set(PlayerStatsType.MaxHp, maxHp);
            Set(PlayerStatsType.MaxMetal, maxMetal);
            Set(PlayerStatsType.MaxCrystal, maxCrystal);
            Set(PlayerStatsType.MaxDeuterium, maxDeuterium);
        }

        public PlayerStats(PlayerStats playerStats)
        {
            foreach (var type in Extension.GetValues<PlayerStatsType>())
            {
                Set(type, playerStats[type]);
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