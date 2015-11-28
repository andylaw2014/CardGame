using Assets.Scripts.Core.Phase;

namespace Assets.Scripts.Core.Message
{
    public class EndPhaseMessage
    {
        public readonly GamePhase Phase;

        public EndPhaseMessage(GamePhase phase)
        {
            Phase = phase;
        }

        public override string ToString()
        {
            return "End Phase:" + Phase;
        }
    }
}