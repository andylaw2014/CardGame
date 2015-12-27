using Assets.Scripts.Core.Phase;

namespace Assets.Scripts.Core.Message
{
    public class PhaseEndMessage : GameMessage
    {
        public readonly BasePhase Phase;

        public PhaseEndMessage(BasePhase phase)
        {
            Phase = phase;
        }
    }
}