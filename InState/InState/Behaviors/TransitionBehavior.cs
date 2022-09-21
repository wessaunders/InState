using InState.Abstracts;
using InState.Interfaces;

namespace InState.Behaviors
{
    public class TransitionBehavior<TStateData, TTriggers>
    {
        private State<TStateData, TTriggers> originatingState;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="trigger">Identifies the trigger that will be associated with the transition</param>
        /// <param name="originatingState">Indicates the originating step</param>
        public TransitionBehavior(TTriggers trigger, State<TStateData, TTriggers> originatingState)
        {
            this.originatingState = originatingState;
            TransitionTrigger = trigger;
        }

        internal IState<TStateData, TTriggers>? PermittedTransition { get; private set; }

        /// <summary>
        /// Define the step the current step can transition to
        /// </summary>
        /// <param name="stateToTransitionTo">Identifies the state that can be transitioned to</param>
        /// <returns>State<T></returns>
        public State<TStateData, TTriggers> TransitionTo(State<TStateData, TTriggers> stateToTransitionTo)
        {
            PermittedTransition = stateToTransitionTo;
            return originatingState;
        }

        internal TTriggers TransitionTrigger { get; private set; }
    }
}
