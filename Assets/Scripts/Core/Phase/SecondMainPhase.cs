namespace Assets.Scripts.Core.Phase
{
    public class SecondMainPhase : GamePhase
    {
        public SecondMainPhase(Game game, Game.User owner) : base(game, owner)
        {
        }

        protected override string Name
        {
            get { return "Second Main Phase"; }
        }

        protected override GamePhase NextPhase
        {
            get { return new ResetPhase(_game, _game.OpponentUser(Owner)); }
        }
    }
}