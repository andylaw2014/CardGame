using System;

namespace Assets.Scripts.Core.Resource
{
    public class ResourceChangeEventArgs : EventArgs
    {
        public readonly int After;
        public readonly int Before;
        public readonly Resource Type;

        public ResourceChangeEventArgs(Resource type, int before, int after)
        {
            Type = type;
            Before = before;
            After = after;
        }
    }
}