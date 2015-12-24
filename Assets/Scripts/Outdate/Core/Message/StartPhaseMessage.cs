using Assets.Scripts.Outdate.Core.Phase;

namespace Assets.Scripts.Outdate.Core.Message
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