using System;

namespace Assets.Scripts.Outdate.Core
{
    public class Card : IEquatable<Card>
    {
        public readonly string Id;
        public readonly string Name;
        private int _metal;
        private int _crystal;
        private int _deuterium;
        private int _hp;

        public Card(Player owner, string name, string id)
        {
            Owner = owner;
            Name = name;
            Id = id;
        }

        public void SetCard(CardController card)
        {
            _metal = card.Metal;
            _crystal = card.Crystal;
            _deuterium = card.Deuterium;
            _hp = card.Hp;
        }

        public Player Owner { get; set; }

        public bool Equals(Card other)
        {
            return Id.Equals(other.Id) && Name.Equals(other.Name);
        }

        public int GetCost(Resource.Resource type)
        {
            switch (type)
            {
                case Resource.Resource.Metal:
                    return _metal;
                case Resource.Resource.Crystal:
                    return _crystal;
                case Resource.Resource.Deuterium:
                    return _deuterium;
                case Resource.Resource.Hp:
                    return _hp;
                default:
                    return 0;
            }
        }
    }
}