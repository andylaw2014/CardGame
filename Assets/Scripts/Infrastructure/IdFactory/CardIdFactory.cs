namespace Assets.Scripts.Infrastructure.IdFactory
{
    public class CardIdFactory : IIdFactory
    {
        public static readonly int FirstPlayer = 0;
        public static readonly int SecondPlayer = 1;
        private readonly int[] _count;

        public CardIdFactory()
        {
            _count = new[] {0, 0};
        }

        public string GetId(int type = 0)
        {
            var prefix = type == FirstPlayer ? "F" : "S";
            return prefix + (_count[type]++);
        }
    }
}