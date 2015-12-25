using Assets.Scripts.Core.Message;

namespace Assets.Scripts.Core.Phase
{
    public abstract class BasePhase
    {
        protected readonly Game Game;

        /// <summary>
        ///     Parent of the phase which can decide when to end phase.
        /// </summary>
        public readonly PlayerType Parent;

        protected BasePhase(Game game, PlayerType parent)
        {
            Game = game;
            Parent = parent;
        }

        protected abstract BasePhase NextPhase { get; }

        /// <summary>
        ///     Start the phase.
        /// </summary>
        public void Start()
        {
            Game.Publish(new PhaseStartMessage(this));
            Execute();
        }

        /// <summary>
        /// To be overriden by child.
        /// Execute after PhaseStartMessage.
        /// </summary>
        protected virtual void Execute()
        {
        }

        /// <summary>
        ///     Move to next Phase.
        /// </summary>
        public void Next()
        {
            Game.Publish(new PhaseEndMessage(this));
            Game.SetPhase(NextPhase);
        }
    }
}