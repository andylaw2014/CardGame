using Assets.Scripts.Core.Message;

namespace Assets.Scripts.Core.Phase
{
    public abstract class BasePhase
    {
        protected readonly Game Game;
        public readonly PlayerType Parent;

        protected BasePhase(Game game, PlayerType parent)
        {
            Game = game;
            Parent = parent;
        }

        public void Start()
        {
            Game.Publish(new PhaseStartMessage(this));
            Execute();
        }

        protected virtual void Execute()
        {
            
        }

        protected abstract BasePhase NextPhase { get; }

        public void Next()
        {
            Game.Publish(new PhaseEndMessage(this));
            Game.SetPhase(NextPhase);
        }
    }
}