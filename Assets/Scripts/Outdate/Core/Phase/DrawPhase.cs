namespace Assets.Scripts.Outdate.Core.Phase
{
    public class DrawPhase : GamePhase
    {
        public DrawPhase(Game game, Game.User owner) : base(game, owner)
        {
            if(owner == Game.User.You)
                game.DrawCardFromDeck();
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