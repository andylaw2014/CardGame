using System;

namespace Assets.Scripts.Core.Event
{
    public class CardChangeEventArg : EventArgs
    {
        public readonly Card Card;

        public CardChangeEventArg(Card card)
        {
            Card = card;
        }
    }
}