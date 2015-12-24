using System;

namespace Assets.Scripts.Gui
{
    public class CardDragToCardEventArgs : EventArgs
    {
        /// <summary>
        /// ID of the card that being drag.
        /// </summary>
        public readonly string Target;

        /// <summary>
        /// ID of the card that being drag to.
        /// </summary>
        public readonly string Destination;
        public CardDragToCardEventArgs(string target, string destination)
        {
            Target = target;
            Destination = destination;
        }
    }
}
