using Assets.Scripts.Utility;

namespace Assets.Scripts.Core.Phase
{
    public class DefencePhase : BasePhase
    {
        public DefencePhase(Game game, PlayerType parent) : base(game, parent)
        {
        }

        protected override BasePhase NextPhase()
        {
            return new SecondMainPhase(Game, Parent.Oopponent());
        }

        public override string GetName()
        {
            return "Defence Phase";
        }
    }
}