namespace Assets.Scripts.Core.Phase
{
    public class DefencePhase : GamePhase
    {
        public DefencePhase(Game game, Game.User owner) : base(game, owner)
        {
        }

        protected override string Name
        {
            get { return "Defence Phase"; }
        }

        protected override GamePhase NextPhase
        {
            get { return new AttackCounterPhase(_game, _game.OpponentUser(Owner)); }
        }
    }
}