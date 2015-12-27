using System;

namespace Assets.Scripts.Core.Event
{
    public class PlayerChangeEventArg : EventArgs
    {
        public readonly Player Player;

        public PlayerChangeEventArg(Player player)
        {
            Player = player;
        }
    }
}
