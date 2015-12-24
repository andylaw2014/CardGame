using Assets.Scripts.Outdate.Core.Resource;

namespace Assets.Scripts.Outdate.Core.Message
{
    public class ResourceChangeMessage
    {
        public readonly ResourceController ResourceController;

        public ResourceChangeMessage(ResourceController resourceController)
        {
            ResourceController = resourceController;
        }

        public override string ToString()
        {
            return "Resource Change:" + ResourceController.Owner.User;
        }
    }
}