using Assets.Scripts.Outdate.Core.Message;
using Assets.Scripts.Outdate.UI.Command;

namespace Assets.Scripts.Outdate.Core.Phase
{
    public abstract class GamePhase
    {
        protected readonly Game _game;
        public readonly Game.User Owner;

        protected GamePhase(Game game, Game.User owner)
        {
            _game = game;
            Owner = owner;
            _game.Publish(new StartPhaseMessage(this));
        }

        protected abstract string Name { get; }
        protected abstract GamePhase NextPhase { get; }

        // Return is handled
        public virtual bool Handle(IUiCommand command)
        {
            return false;
        }

        public void Next()
        {
            _game.Publish(new EndPhaseMessage(this));
            _game.SetPhase(NextPhase);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}