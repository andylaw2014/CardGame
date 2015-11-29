namespace Assets.Scripts.Core.Message
{
    public class DrawCardMessage
    {
        public readonly Card Card;
        public readonly Player Player;

        public DrawCardMessage(Player player, Card card)
        {
            Player = player;
            Card = card;
        }

        public override string ToString()
        {
            return "DrawCard:" + Player + " => " + Card;
        }
    }
}