namespace Assets.Scripts.Outdate.Core.Phase
{
    public class DefenceCounterPhase : GamePhase
    {
        public DefenceCounterPhase(Game game, Game.User owner) : base(game, owner)
        {
        }

        protected override string Name
        {
            get { return "Defence Counter Phase"; }
        }

        protected override GamePhase NextPhase
        {
            get { return new SecondMainPhase(_game, _game.OpponentUser(Owner)); }
        }
    }
}
