namespace Assets.Scripts.Outdate.Core.Message
{
    public class PlayCardMessage
    {
        public readonly Card Card;
        public readonly Player Player;

        public PlayCardMessage(Player player, Card card)
        {
            Player = player;
            Card = card;
        }

        public override string ToString()
        {
            return "PlayCard:" + Player + " => " + Card;
        }
    }
}