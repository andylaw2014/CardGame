using Assets.Scripts.Core.Message;
using Assets.Scripts.UI.Command;

namespace Assets.Scripts.Core.Phase
{
    public abstract class GamePhase
    {
        public readonly Game.User Owner;
        protected abstract string Name { get; }
        protected abstract GamePhase NextPhase { get; }
        protected readonly Game _game;

        protected GamePhase(Game game, Game.User owner)
        {
            _game = game;
            Owner = owner;
            _game.Publish(new StartPhaseMessage(this));
        }

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