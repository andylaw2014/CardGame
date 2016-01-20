namespace Assets.Scripts.Core.Message
{
    public class CardStatsChangeMessage : GameMessage
    {
        public readonly Card Card;

        public CardStatsChangeMessage(Card card)
        {
            Card = card;
        }
    }
}