namespace Core.Zone
{
    public interface IZone
    {
        Zone Name { get; }
        void Add(Card card);
        void Remove(Card card);
    }
}