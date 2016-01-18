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

        protected abstract BasePhase NextPhase();

        /// <summary>
        ///     Start the phase.
        /// </summary>
        public void Start()
        {
            Execute();
        }

        /// <summary>
        ///     To be overriden by child.
        ///     Execute after PhaseStartMessage.
        /// </summary>
        protected virtual void Execute()
        {
        }

        /// <summary>
        ///     Move to next Phase.
        /// </summary>
        public void Next()
        {
            Game.SetPhase(NextPhase());
        }

        /// <summary>
        ///     Get the name of phase.
        /// </summary>
        /// <returns></returns>
        public abstract string GetName();
    }
}