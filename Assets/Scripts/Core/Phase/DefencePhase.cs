using Assets.Scripts.Gui.Event;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Core.Phase
{
    public class DefencePhase : BasePhase
    {
        public DefencePhase(Game game, PlayerType parent) : base(game, parent)
        {
        }

        protected override void Execute()
        {
            Game.ShowAttacker();
            if (Parent != PlayerType.Player) return;
            Game.SelectDefender();
        }

        protected override BasePhase NextPhase()
        {
            return new SecondMainPhase(Game, Parent.Opposite());
        }

        public override string GetName()
        {
            return "Defence Phase";
        }

        public virtual void Handle(CardDragToCardEventArgs args)
        {
            if (Game.GetCardById(args.Target).Zone != ZoneType.BattleField) return;
            if (Game.GetCardById(args.Destination).Zone != ZoneType.BattleField) return;
            Game.AddDefender(args.Target, args.Destination);
        }
    }
}