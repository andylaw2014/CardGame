using System;
using Assets.Scripts.Core.Phase;

namespace Assets.Scripts.Core.Event
{
    public class PhaseChangeEventArg : EventArgs
    {
        public readonly BasePhase Phase;

        public PhaseChangeEventArg(BasePhase phase)
        {
            Phase = phase;
        }
    }
}