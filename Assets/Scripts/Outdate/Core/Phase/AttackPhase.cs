
namespace Assets.Scripts.Outdate.Core.Phase
{
    public class AttackPhase : GamePhase
    {
        public AttackPhase(Game game, Game.User owner) : base(game, owner)
        {
        }

        protected override string Name
        {
            get { return "Attack Phase"; }
        }

        protected override GamePhase NextPhase
        {
            get { return new DefencePhase(_game, _game.OpponentUser(Owner)); }
        }
    }
}