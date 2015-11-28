using Assets.Scripts.Core.Message;
using Assets.Scripts.Core.Resource;
using Assets.Scripts.Core.Zone;

namespace Assets.Scripts.Core
{
    public class Player
    {
        private readonly BattlefieldZone _battlefield;
        private readonly Game _game;
        private readonly HandZone _hand;
        private readonly Game.User _user;
        private readonly ResourceController _resourceController;
        private readonly ResourceController _maxResourceController;

        public Player(Game game, Game.User user)
        {
            _game = game;
            _user = user;
            _hand = new HandZone(game, this);
            _battlefield = new BattlefieldZone(game, this);
            _resourceController = new ResourceController();
            _maxResourceController = new ResourceController();
            _resourceController.ResourceChange += OnResourceChange;
            _maxResourceController.ResourceChange += OnMaxResourceChange;
        }

        public void DrawCard(Card card)
        {
            _game.Publish(new DrawCardMessage(this,card));
            _hand.Add(card);
        }

        public Game.User GetUser()
        {
            return _user;
        }

        private void OnResourceChange(object sender, ResourceChangeEventArgs args)
        {
            _game.Publish(new ResourceChangeMessage(this,args));
        }

        private void OnMaxResourceChange(object sender, ResourceChangeEventArgs args)
        {
            _game.Publish(new MaxResourceChangeMessage(this, args));
        }
    }
}