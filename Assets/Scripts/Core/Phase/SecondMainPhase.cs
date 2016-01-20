using Assets.Scripts.Utility;

namespace Assets.Scripts.Core.Phase
{
    public class SecondMainPhase : BasePhase
    {
        public SecondMainPhase(Game game, PlayerType parent) : base(game, parent)
        {
        }

        protected override BasePhase NextPhase()
        {
            return new MainPhase(Game, Parent.Opposite());
        }

        public override string GetName()
        {
            return "Second Main Phase";
        }

        public override bool AllowPlayCard()
        {
            return Parent == PlayerType.Player;
        }
    }
}