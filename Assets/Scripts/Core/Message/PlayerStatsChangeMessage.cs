namespace Assets.Scripts.Core.Message
{
    public class PlayerStatsChangeMessage : GameMessage
    {
        public readonly Player Player;

        public PlayerStatsChangeMessage(Player player)
        {
            Player = player;
        }
    }
}
