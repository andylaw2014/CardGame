namespace Assets.Scripts.Core.Message
{
    public class CardParentChangeMessage : GameMessage
    {
        public readonly Card Card;

        public CardParentChangeMessage(Card card)
        {
            Card = card;
        }
    }
}