using Assets.Scripts.Core.Phase;

namespace Assets.Scripts.Core.Message
{
    class PhaseEndMessage
    {
        public readonly BasePhase Phase;

        public PhaseEndMessage(BasePhase phase)
        {
            Phase = phase;
        }
    }
}
