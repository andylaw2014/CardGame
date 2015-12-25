using System;
using Assets.Scripts.Core;

namespace Assets.Scripts.Gui.Event
{
    public class ButtonClickEventArgs : EventArgs
    {
        public readonly ButtonType Type;

        public ButtonClickEventArgs(ButtonType type)
        {
            Type = type;
        }
    }
}