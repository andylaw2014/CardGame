namespace Assets.Scripts.Core.Phase
{
    public class MainPhase : BasePhase
    {
        public MainPhase(Game game, PlayerType parent) : base(game, parent)
        {
        }

        protected override void Execute()
        {
            if (Parent != PlayerType.Player) return;
            Game.AddResourceByPanel(Parent);
            Game.DrawCard(Parent);
        }

        protected override BasePhase NextPhase()
        {
            return new AttackPhase(Game, Parent);
        }

        public override string GetName()
        {
            return "Main Phase";
        }

        public override bool AllowPlayCard()
        {
            return Parent == PlayerType.Player;
        }
    }
}