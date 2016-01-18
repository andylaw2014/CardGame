namespace Assets.Scripts.Core.Message
{
    public class EnableResourcePanelMessage : GameMessage
    {
        public readonly bool EnableCrystal;
        public readonly bool EnableDeuterium;
        public readonly bool EnableMetal;
        public readonly PlayerType Player;

        public EnableResourcePanelMessage(PlayerType player, bool enableMetal, bool enableCrystal, bool enableDeuterium)
        {
            Player = player;
            EnableMetal = enableMetal;
            EnableCrystal = enableCrystal;
            EnableDeuterium = enableDeuterium;
        }
    }
}