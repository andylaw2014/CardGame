namespace Assets.Scripts.Outdate.Infrastructure
{
    public class IdFactory
    {
        private readonly bool _type;
        private int _current;

        public IdFactory(bool type)
        {
            _type = type;
            _current = 0;
        }

        public string Generate()
        {
            var id = _type ? "T" : "F";
            id += _current;
            _current++;
            return id;
        }
    }
}