using Assets.Scripts.Core.Zone;

namespace Assets.Scripts.Core
{
    public class Card
    {
        public Card(Player Owner)
        {
            this.Owner = Owner;
        }

        public Player Owner { get; set; }
        public OrderedZone Zone { get; private set; }

        public void ChangeZone(OrderedZone zone)
        {
            Zone = zone;
        }
    }
}