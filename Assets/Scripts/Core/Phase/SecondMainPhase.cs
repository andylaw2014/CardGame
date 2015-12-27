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
            return new MainPhase(Game, Parent.Oopponent());
        }

        public override string GetName()
        {
            return "Second Main Phase";
        }
    }
}