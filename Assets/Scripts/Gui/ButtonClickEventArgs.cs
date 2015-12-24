using System;
using Assets.Scripts.Core;

namespace Assets.Scripts.Gui
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