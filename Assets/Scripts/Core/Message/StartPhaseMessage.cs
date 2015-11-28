using Assets.Scripts.Core.Phase;

namespace Assets.Scripts.Core.Message
{
    public class StartPhaseMessage
    {
        public readonly GamePhase Phase;

        public StartPhaseMessage(GamePhase phase)
        {
            Phase = phase;
        }

        public override string ToString()
        {
            return "Start Phase:" + Phase;
        }
    }
}