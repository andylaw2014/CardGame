using System;

namespace Assets.Scripts.Core.Resource
{
    public class ResourceController
    {
        private readonly int[] _count;

        public ResourceController()
        {
            _count = new[] {0, 0, 0};
        }

        public event EventHandler<ResourceChangeEventArgs> ResourceChange = (sender, args) => { };

        public int GetResource(Resource resource)
        {
            return _count[(int) resource];
        }

        public void SetResource(Resource resource, int value)
        {
            var before = _count[(int) resource];
            _count[(int) resource] = value;
            ResourceChange(this, new ResourceChangeEventArgs(resource, before, value));
        }
    }
}