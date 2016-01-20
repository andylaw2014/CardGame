namespace Assets.Scripts.Core.Message
{
    public class CardDeadMessage : GameMessage
    {
        public readonly Card Card;

        public CardDeadMessage(Card card)
        {
            Card = card;
        }
    }
}