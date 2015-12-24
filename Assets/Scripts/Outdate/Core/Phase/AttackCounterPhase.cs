namespace Assets.Scripts.Outdate.Core.Phase
{
    public class AttackCounterPhase : GamePhase
    {
        public AttackCounterPhase(Game game, Game.User owner) : base(game, owner)
        {
        }

        protected override string Name
        {
            get { return "Attack Counter Phase"; }
        }

        protected override GamePhase NextPhase
        {
            get { return new DefenceCounterPhase(_game, _game.OpponentUser(Owner)); }
        }
    }
}