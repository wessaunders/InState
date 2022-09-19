using InState.Abstracts;
using InState.Enums;

namespace InState.Behaviors
{
    public class TransitionBehavior<T>
    {
        private State<T> originatingState;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="trigger">Identifies the trigger that will be associated with the transition</param>
        /// <param name="originatingState">Indicates the originating state</param>
        public TransitionBehavior(Triggers trigger, State<T> originatingState)
        {
            this.originatingState = originatingState;
            TransitionTrigger = trigger;
        }

        internal State<T>? PermittedTransition { get; private set; }

        /// <summary>
        /// Define the state the current state can transition to
        /// </summary>
        /// <param name="stateToTransitionTo">Identifies the state that can be transitioned to</param>
        /// <returns>State<T></returns>
        public State<T> TransitionTo(State<T> stateToTransitionTo)
        {
            PermittedTransition = stateToTransitionTo;
            return originatingState;
        }

        internal Triggers TransitionTrigger { get; private set; }
    }
}
