using Assets.Scripts.Utility;

namespace Assets.Scripts.Core.Phase
{
    public class AttackPhase : BasePhase
    {
        public AttackPhase(Game game, PlayerType parent) : base(game, parent)
        {
        }

        protected override BasePhase NextPhase()
        {
            return new DefencePhase(Game, Parent.Opposite());
        }

        public override string GetName()
        {
            return "Attack Phase";
        }
    }
}