using System.Collections.Generic;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Outdate.Core.Message;
using Assets.Scripts.Outdate.Core.Resource;

namespace Assets.Scripts.Outdate.Core
{
    public class Player
    {
        private readonly List<Card> _battlefield;
        private readonly List<Card> _hand;
        private readonly ResourceController _resourceController;
        public readonly Game Game;
        private readonly int MaximumResource = 10;
        public readonly Game.User User;

        public Player(Game game, Game.User user)
        {
            Game = game;
            User = user;
            _hand = new List<Card>();
            _battlefield = new List<Card>();
            _resourceController = new ResourceController(this);
        }

        public void DrawCard(Card card)
        {
            Log.Verbose("Player " + User + ": Draw Card");
            _hand.Add(card);
            Game.Publish(new DrawCardMessage(this, card));
        }

        public void PlayCard(Card card)
        {
            Log.Verbose("Try Play Card:" + card.Id);
            if (!_hand.Remove(card)) return;
            RemoveCost(card, Resource.Resource.Metal);
            RemoveCost(card, Resource.Resource.Crystal);
            RemoveCost(card, Resource.Resource.Deuterium);
            _battlefield.Add(card);
            Log.Verbose("Play Card:" + card.Id);
            Game.Publish(new PlayCardMessage(this, card));
        }

        public bool IsPlayable(Card card)
        {
            Log.Verbose("IsPlayable:" + card.Id);
            if (!_hand.Contains(card)) return false;
            var playable = CheckCost(card, Resource.Resource.Metal);
            playable = playable && CheckCost(card, Resource.Resource.Crystal);
            playable = playable && CheckCost(card, Resource.Resource.Deuterium);
            return playable;
        }

        public void AddResource(Resource.Resource resource, bool reset = false)
        {
            var max = _resourceController.GetResource(resource, ResourceController.Type.Maximum);
            _resourceController.SetResource(resource, ResourceController.Type.Maximum, max + 1);
            if (reset)
                _resourceController.RestoreAll();
        }

        public bool IsResourceFull(Resource.Resource resource)
        {
            return _resourceController.GetResource(resource, ResourceController.Type.Maximum) >= MaximumResource;
        }

        private bool CheckCost(Card card, Resource.Resource resource)
        {
            return _resourceController.GetResource(resource, ResourceController.Type.Current) >=
                   card.GetCost(resource);
        }

        public void ResetCost()
        {
            _resourceController.RestoreAll();
        }

        private void RemoveCost(Card card, Resource.Resource resource)
        {
            var current = _resourceController.GetResource(resource, ResourceController.Type.Current);
            var cost = card.GetCost(resource);
            _resourceController.SetResource(resource, ResourceController.Type.Current, current - cost);
        }

        public void Update()
        {
            _resourceController.Update();
        }
    }
}