namespace Assets.Scripts.Core.Phase
{
    public class DrawPhase : GamePhase
    {
        public DrawPhase(Game game, Game.User owner) : base(game, owner)
        {
        }

        protected override string Name
        {
            get { return "Draw Phase"; }
        }

        protected override GamePhase NextPhase
        {
            get { return new MainPhase(_game, Owner); }
        }
    }
}