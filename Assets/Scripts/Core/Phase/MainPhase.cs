namespace Assets.Scripts.Core.Phase
{
    public class MainPhase : GamePhase
    {
        public MainPhase(Game game, Game.User owner) : base(game, owner)
        {
        }

        protected override string Name
        {
            get { return "Main Phase"; }
        }

        protected override GamePhase NextPhase
        {
            get { return new AttackPhase(_game, Owner); }
        }
    }
}