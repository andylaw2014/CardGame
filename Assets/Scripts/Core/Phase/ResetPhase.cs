namespace Assets.Scripts.Core.Phase
{
    public class ResetPhase : GamePhase
    {
        public ResetPhase(Game game, Game.User owner) : base(game, owner)
        {
        }

        protected override string Name
        {
            get { return "Reset Phase"; }
        }

        protected override GamePhase NextPhase
        {
            get { return new DrawPhase(_game, Owner); }
        }
    }
}