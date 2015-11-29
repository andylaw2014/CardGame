using Assets.Scripts.Infrastructure;
using Assets.Scripts.UI.Command;

namespace Assets.Scripts.Core.Phase
{
    public class MainPhase : GamePhase
    {
        public MainPhase(Game game, Game.User owner) : base(game, owner)
        {
        }


        public override bool Handle(IUiCommand command)
        {
            Log.Verbose("Main Phase Handle: IUiCommand");
            if (Owner != Game.User.You)
                return false;
            var dragCommand = command as DragCommand;
            if (dragCommand == null) return false;
            var id = dragCommand.Source.name;
            var card =_game.GetCardById(id);
            if (!card.Owner.IsPlayable(card)) return false;
            _game.GameController.RpcPlayCard(id);
            return true;
        }

        protected override string Name
        {
            get { return "Main Phase"; }
        }

        protected override GamePhase NextPhase
        {
            get { return new AttackPhase(_game, Owner); }
        }
    }
}