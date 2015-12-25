namespace Assets.Scripts.Outdate.Core.Phase
{
    public class ResetPhase : GamePhase
    {
        public ResetPhase(Game game, Game.User owner) : base(game, owner)
        {
            if (owner == Game.User.You)
                game.GameController.GuiController.ShowResourcePanelController();
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