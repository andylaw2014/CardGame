using Assets.Scripts.Core.Phase;

namespace Assets.Scripts.Core.Message
{
    public class PhaseStartMessage
    {
        public readonly BasePhase Phase;

        public PhaseStartMessage(BasePhase phase)
        {
            Phase = phase;
        }
    }
}