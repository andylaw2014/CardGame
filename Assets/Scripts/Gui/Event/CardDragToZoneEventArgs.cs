using System;
using Assets.Scripts.Core;

namespace Assets.Scripts.Gui.Event
{
    public class CardDragToZoneEventArgs : EventArgs
    {
        /// <summary>
        ///     The Zone that being drag to.
        /// </summary>
        public readonly ZoneType Destination;

        /// <summary>
        ///     Owner of destination.
        /// </summary>
        public readonly PlayerType Owner;

        /// <summary>
        ///     ID of the card that being drag.
        /// </summary>
        public readonly string Target;

        public CardDragToZoneEventArgs(string target, ZoneType destination, PlayerType owner)
        {
            Target = target;
            Destination = destination;
            Owner = owner;
        }
    }
}