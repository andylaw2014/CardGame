using System.Collections;
using System.Collections.Generic;
using Infrastructure;

namespace Core.Zone
{
    public abstract class OrderedZone : IEnumerable<Card>
    {
        public readonly Player Owner;
        public abstract Zone Name { get; }
        protected readonly Game Game;
        private readonly List<Card> _cards;

        OrderedZone(Game game, Player owner)
        {
            Game = game;
            Owner = owner;
            _cards = new List<Card>();
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Card card)
        {
            _cards.Add(card);
            AfterAdd(card);
        }

        public void AddFront(Card card)
        {
            _cards.Insert(0, card);
            AfterAdd(card);
        }

        protected abstract void AfterAdd(Card card);

        public void Remove(Card card)
        {
            _cards.Remove(card);
            AfterRemove(card);
        }

        protected Card PopTop()
        {
            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }

        protected abstract void AfterRemove(Card card);

        public void Shuffle()
        {
            _cards.Shuffle();
        }
    }
}
