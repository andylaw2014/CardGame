using Assets.Scripts.Core.Message;

namespace Assets.Scripts.Core.Resource
{
    public class ResourceController
    {

        public enum Type
        {
            Current, Maximum
        }

        public readonly Player Owner;
        private readonly int[][] _count;

        public ResourceController(Player owner)
        {
            Owner = owner;
            _count = new int [2][];
            for(var i=0;i< _count.Length;i++)
                _count[i] = new [] {0,0,0};
        }

        public void Update()
        {
            Owner.Game.Publish(new ResourceChangeMessage(this));
        }

        public int GetResource(Resource resource,Type type)
        {
            return _count[(int) type][(int) resource];
        }

        public void SetResource(Resource resource, Type type, int value)
        {
            _count[(int)type][(int)resource] = value;
            Update();
        }

        public void RestoreAll()
        {
            for (var i = 0; i < _count[0].Length; i++)
                    _count[(int)Type.Current][i] = _count[(int)Type.Maximum][i];
        }
    }
}