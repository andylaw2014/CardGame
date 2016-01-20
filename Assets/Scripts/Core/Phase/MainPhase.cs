using Assets.Scripts.Gui.Event;

namespace Assets.Scripts.Core.Phase
{
    public class MainPhase : BasePhase
    {
        public MainPhase(Game game, PlayerType parent) : base(game, parent)
        {
        }

        protected override void Execute()
        {
            Game.ResetBattle();
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

        public override void Handle(CardDragToZoneEventArgs args)
        {
            if (Parent != PlayerType.Player || args.Destination != ZoneType.BattleField) return;
            Game.TryPlay(args.Target);
        }
    }
}