namespace Assets.Scripts.Core.Message
{
    public class CardZoneChangeMessage : GameMessage
    {
        public readonly Card Card;

        public CardZoneChangeMessage(Card card)
        {
            Card = card;
        }
    }
}