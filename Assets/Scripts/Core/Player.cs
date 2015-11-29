using Assets.Scripts.Core.Message;
using Assets.Scripts.Core.Resource;
using Assets.Scripts.Core.Zone;

namespace Assets.Scripts.Core
{
    public class Player
    {
        public readonly Game Game;
        private readonly BattlefieldZone _battlefield;
        private readonly HandZone _hand;
        public readonly Game.User User;
        private readonly ResourceController _resourceController;

        public Player(Game game, Game.User user)
        {
            Game = game;
            User = user;
            _hand = new HandZone(game, this);
            _battlefield = new BattlefieldZone(game, this);
            _resourceController = new ResourceController(this);
        }

        public void DrawCard(Card card)
        {
            Game.Publish(new DrawCardMessage(this,card));
            _hand.Add(card);
        }

        public void Update()
        {
            _resourceController.Update();
        }
    }
}