using System;

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