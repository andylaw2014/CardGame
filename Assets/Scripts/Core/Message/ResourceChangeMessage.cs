using Assets.Scripts.Core.Resource;

namespace Assets.Scripts.Core.Message
{
    public class ResourceChangeMessage
    {
        public readonly ResourceChangeEventArgs Args;
        public readonly Player Player;

        public ResourceChangeMessage(Player player, ResourceChangeEventArgs args)
        {
            Player = player;
            Args = args;
        }

        public override string ToString()
        {
            return "Resource Change:" + Player + "." + Args.Type + ":" +
                   Args.Before + " => " + Args.After;
        }
    }

    public class MaxResourceChangeMessage : ResourceChangeMessage
    {
        public MaxResourceChangeMessage(Player player, ResourceChangeEventArgs args) :
            base(player, args)
        {
        }

        public override string ToString()
        {
            return "Max Resource Change:" + Player + "." + Args.Type + ":" +
                   Args.Before + " => " + Args.After;
        }
    }
}