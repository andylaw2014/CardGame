namespace Assets.Scripts.Core.Message
{
    public class EnableResourcePanelMessage : GameMessage
    {
        public readonly PlayerType Player;
        public readonly bool EnableMetal;
        public readonly bool EnableCrystal;
        public readonly bool EnableDeuterium;

        public EnableResourcePanelMessage(PlayerType player, bool enableMetal, bool enableCrystal, bool enableDeuterium)
        {
            Player = player;
            EnableMetal = enableMetal;
            EnableCrystal = enableCrystal;
            EnableDeuterium = enableDeuterium;
        }
    }
}